import { EventModel } from "./EventModel";
import { SpeakerModel } from "./SpeakerModel";

export interface SocialNetworkModel{
  id: number;
  name: string;
  url: string;
  eventId: number;
  speakerId: number;
}
