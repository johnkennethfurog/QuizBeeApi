import { Category } from './category';

export interface Question {
    timeLimit:number;
    category?: Category;
    categoryId?:number;
    question: string;
    answer: string;
    type: number;
    id?: number;
    point: number;
    questionChoices: string[];
    eventId: number;
}
