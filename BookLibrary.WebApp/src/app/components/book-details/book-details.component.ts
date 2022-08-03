import { Component, OnInit } from '@angular/core';
import {Book} from "../../interfaces/book";
import {ActivatedRoute} from "@angular/router";
import {BookService} from "../../services/book.service";
import {Observable, tap} from "rxjs";

@Component({
  selector: 'app-book-details',
  templateUrl: './book-details.component.html',
  styleUrls: ['./book-details.component.css']
})
export class BookDetailsComponent implements OnInit {

  book$!: Observable<Book>;
  bookInfo: Map<string, string | number> = new Map<string, string>();

  originalOrder = () => 0

  constructor(private route: ActivatedRoute, private bookService: BookService) { }

  ngOnInit(): void {
    const bookId = this.route.snapshot.paramMap.get('bookId');

    if(bookId === null){
      return;
    }

    this.book$ = this.bookService.getBook(bookId)
      .pipe(
        tap(book => {
          this.bookInfo.set('Autor', book.author);
          this.bookInfo.set('Wydawnictwo', book.publisherName);
          this.bookInfo.set('Rok wydania', book.releaseYear ? book.releaseYear : 'Nieznany');
          this.bookInfo.set('Liczba stron', book.numberOfPages);
          this.bookInfo.set('Język', book.language);
        }),
      )
  }
}
