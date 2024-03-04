export interface LotModel{
  id: number;
  name: string;
  price: number;
  initialDate?: Date;
  finalDate?: Date;
  quantity: number;
  eventId: number;
  event: Event;
}
