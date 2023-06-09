import { User } from "./user";
import { User2 } from "./user2";

export interface Review {
  id: string;
  title: string;
  content: string;
  rating: number;
  bookId: string;
  userId: string;
  user: User2;
  dateCreated: Date;
  dateModified: Date;
  showEdit: boolean;
  editForm: any;
}
