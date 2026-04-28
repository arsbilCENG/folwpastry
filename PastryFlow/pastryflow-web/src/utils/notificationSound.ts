class NotificationSound {
  private audioContext: AudioContext | null = null;

  private getContext(): AudioContext {
    if (!this.audioContext) {
      this.audioContext = new AudioContext();
    }
    return this.audioContext;
  }

  play(): void {
    try {
      const ctx = this.getContext();
      
      // İki tonlu bildirim sesi oluştur
      const now = ctx.currentTime;
      
      // Ton 1
      const osc1 = ctx.createOscillator();
      const gain1 = ctx.createGain();
      osc1.type = 'sine';
      osc1.frequency.setValueAtTime(880, now); // A5
      gain1.gain.setValueAtTime(0.3, now);
      gain1.gain.exponentialRampToValueAtTime(0.01, now + 0.15);
      osc1.connect(gain1);
      gain1.connect(ctx.destination);
      osc1.start(now);
      osc1.stop(now + 0.15);

      // Ton 2
      const osc2 = ctx.createOscillator();
      const gain2 = ctx.createGain();
      osc2.type = 'sine';
      osc2.frequency.setValueAtTime(1174.66, now + 0.15); // D6
      gain2.gain.setValueAtTime(0.3, now + 0.15);
      gain2.gain.exponentialRampToValueAtTime(0.01, now + 0.3);
      osc2.connect(gain2);
      gain2.connect(ctx.destination);
      osc2.start(now + 0.15);
      osc2.stop(now + 0.3);
    } catch {
      // Audio API desteklenmiyorsa sessizce devam et
    }
  }

  // Browser Notification API ile masaüstü bildirimi
  async requestPermission(): Promise<boolean> {
    if (!('Notification' in window)) return false;
    
    if (Notification.permission === 'granted') return true;
    if (Notification.permission === 'denied') return false;
    
    const permission = await Notification.requestPermission();
    return permission === 'granted';
  }

  async showBrowserNotification(title: string, body: string): Promise<void> {
    const hasPermission = await this.requestPermission();
    if (!hasPermission) return;

    try {
      const notification = new Notification(title, {
        body,
        icon: '/logo192.png', // varsa
        badge: '/logo192.png',
        tag: 'pastryflow-notification',
        requireInteraction: false,
      });

      // 5 saniye sonra otomatik kapat
      setTimeout(() => notification.close(), 5000);

      // Tıklayınca uygulamaya odaklan
      notification.onclick = () => {
        window.focus();
        notification.close();
      };
    } catch {
      // Sessizce devam et
    }
  }
}

const notificationSound = new NotificationSound();
export default notificationSound;
