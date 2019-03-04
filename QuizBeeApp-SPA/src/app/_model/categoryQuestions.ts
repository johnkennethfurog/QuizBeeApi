import { Category } from './category';
import { Question } from './question';

export interface CategoryQuestions {
    category:Category;
    questions:Question[];
}
