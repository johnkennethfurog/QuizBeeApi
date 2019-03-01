import { Judge } from './judge';
import { Participant } from './participant';
import { Question } from './question';

export interface QuizbeeEvent {
    id: number;
    name: string;
    code: string;
    judges?: Judge[];
    participants?: Participant[];
    quizItems?: Question[];
}
