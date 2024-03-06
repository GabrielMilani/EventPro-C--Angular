import { LotModel } from "./LotModel";
import { SocialNetworkModel } from "./SocialNetworkModel";
import { SpeakerModel } from "./SpeakerModel";

export interface EventModel{
  id: number;
  theme: string;
  local: string;
  telephone: string;
  imageUrl: string;
  email: string;
  quantityPeople: number;
  eventDate?: Date;
  lots: LotModel[];
  socialNetworks: SocialNetworkModel[];
  speakerEvents: SpeakerModel[];
}

