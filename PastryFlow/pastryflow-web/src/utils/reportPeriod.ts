import dayjs, { type Dayjs } from 'dayjs';
import type { PeriodPreset } from '../types/report';

export const getDateRange = (preset: PeriodPreset): [Dayjs, Dayjs] => {
  const today = dayjs();
  switch (preset) {
    case 'this_week':
      return [today.startOf('week'), today];
    case 'this_month':
      return [today.startOf('month'), today];
    case 'last_month':
      return [
        today.subtract(1, 'month').startOf('month'),
        today.subtract(1, 'month').endOf('month'),
      ];
    default:
      return [today.startOf('month'), today];
  }
};
