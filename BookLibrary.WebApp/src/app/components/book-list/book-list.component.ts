import { Component, OnInit } from '@angular/core';
import {Book} from "../../interfaces/book";
import {BookService} from "../../services/book.service";
import {Observable} from "rxjs";

@Component({
  selector: 'app-book-list',
  templateUrl: './book-list.component.html',
  styleUrls: ['./book-list.component.css']
})
export class BookListComponent implements OnInit {

  books: Book[] = [];
  filteredBooks: Book[] = [];
  obook!: Observable<Book[]>;

  // books: Book[] = [
  //   {title: "some title1", url: "https://static01.helion.com.pl/global/okladki/326x466/ukrcza.jpg", searchValue: "some title1"},
  //   {title: "some title2", url: "https://static01.helion.com.pl/global/okladki/326x466/ukrcza.jpg", searchValue: "some title2"},
  //   {title: "some title3 lorem impsum asiasissssjjjjhhhaisnianinaisninais", url: "https://static01.helion.com.pl/global/okladki/326x466/ukrcza.jpg", searchValue: "some title1"},
  //   {title: "some title", url: "https://static01.helion.com.pl/global/okladki/326x466/ukrcza.jpg", searchValue: "some title3"},
  //   {title: "some title", url: "https://static01.helion.com.pl/global/okladki/326x466/ukrcza.jpg", searchValue: "some title4"},
  //   {title: "some title", url: "https://static01.helion.com.pl/global/okladki/326x466/ukrcza.jpg", searchValue: "some title5"},
  //   {title: "some title", url: "https://static01.helion.com.pl/global/okladki/326x466/ukrcza.jpg", searchValue: "some title6"},
  // ]


  constructor(private bookService: BookService) { }

  ngOnInit(): void {
    this.bookService.getBooks()
      .subscribe((books: Book[]) => {
        this.books = books;
        this.filteredBooks = this.books;
        console.log(books)
      })
    // this.obook = this.bookService.getBooks();
  }

  toBooks(objects: object[]) {
    if(objects.length > 0 && "title" in objects[0]) {
      return objects as Book[];
    } else {
      return [];
    }
  }
}
