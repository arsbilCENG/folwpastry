import * as signalR from '@microsoft/signalr';
import type { NotificationDto } from '../types/notification';

type NotificationCallback = (notification: NotificationDto) => void;

class SignalRService {
  private connection: signalR.HubConnection | null = null;
  private notificationCallbacks: NotificationCallback[] = [];
  private reconnectAttempts = 0;
  private maxReconnectAttempts = 10;

  async start(token: string): Promise<void> {
    if (this.connection) {
      await this.stop();
    }

    // SignalR bağlantı URL'i — nginx /hubs/ proxy'si üzerinden
    const hubUrl = `${window.location.origin}/hubs/notifications`;

    this.connection = new signalR.HubConnectionBuilder()
      .withUrl(hubUrl, {
        accessTokenFactory: () => token,
        transport: signalR.HttpTransportType.WebSockets | signalR.HttpTransportType.LongPolling,
      })
      .withAutomaticReconnect({
        nextRetryDelayInMilliseconds: (retryContext) => {
          // Exponential backoff: 1s, 2s, 4s, 8s, 16s, max 30s
          if (retryContext.previousRetryCount >= this.maxReconnectAttempts) {
            return null; // Bağlantıyı kes
          }
          return Math.min(1000 * Math.pow(2, retryContext.previousRetryCount), 30000);
        },
      })
      .configureLogging(signalR.LogLevel.Warning)
      .build();

    // Event handlers
    this.connection.on('ReceiveNotification', (notification: NotificationDto) => {
      this.notificationCallbacks.forEach(cb => cb(notification));
    });

    this.connection.onreconnecting(() => {
      console.log('SignalR: Yeniden bağlanılıyor...');
    });

    this.connection.onreconnected(() => {
      console.log('SignalR: Yeniden bağlandı');
      this.reconnectAttempts = 0;
    });

    this.connection.onclose(() => {
      console.log('SignalR: Bağlantı kapandı');
    });

    try {
      await this.connection.start();
      console.log('SignalR: Bağlantı kuruldu');
      this.reconnectAttempts = 0;
    } catch (error) {
      console.error('SignalR: Bağlantı hatası', error);
    }
  }

  async stop(): Promise<void> {
    if (this.connection) {
      try {
        await this.connection.stop();
      } catch {
        // Sessizce devam et
      }
      this.connection = null;
    }
  }

  onNotification(callback: NotificationCallback): () => void {
    this.notificationCallbacks.push(callback);
    // Unsubscribe fonksiyonu döndür
    return () => {
      this.notificationCallbacks = this.notificationCallbacks.filter(cb => cb !== callback);
    };
  }

  getConnectionState(): signalR.HubConnectionState | null {
    return this.connection?.state ?? null;
  }

  isConnected(): boolean {
    return this.connection?.state === signalR.HubConnectionState.Connected;
  }
}

// Singleton instance
const signalRService = new SignalRService();
export default signalRService;
