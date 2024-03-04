import { LotModel } from "./LotModel";
import { SocialNetworkModel } from "./SocialNetworkModel";
import { SpeakerModel } from "./SpeakerModel";

export interface EventModel{
  id: number;
  local: string;
  eventDate?: Date;
  theme: string;
  quantityPeople: number;
  imageUrl: string;
  email: string;
  lots: LotModel[];
  socialNetworks: SocialNetworkModel[];
  speakerEvents: SpeakerModel[];
}

