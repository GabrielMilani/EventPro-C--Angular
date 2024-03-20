import { EventModel } from "./EventModel";
import { SocialNetworkModel } from "./SocialNetworkModel";
import { UserUpdate } from "./identity/UserUpdate";

export interface SpeakerModel{
  id: number;
  miniCV: string;
  user: UserUpdate;
  socialNetworks: SocialNetworkModel[];
  speakerEvents: EventModel[];
}
