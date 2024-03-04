import { EventModel } from "./EventModel";
import { SocialNetworkModel } from "./SocialNetworkModel";

export interface SpeakerModel{
  id: number;
  name: string;
  miniCV: string;
  imageUrl: string;
  telephone: string;
  email: string;
  socialNetworks: SocialNetworkModel[];
  speakerEvents: EventModel[];
}
