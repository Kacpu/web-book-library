import { Component, OnInit } from '@angular/core';
import {Book} from "../../interfaces/book";
import {ActivatedRoute} from "@angular/router";
import {BookService} from "../../services/book.service";
import {Observable} from "rxjs";

@Component({
  selector: 'app-book-details',
  templateUrl: './book-details.component.html',
  styleUrls: ['./book-details.component.css']
})
export class BookDetailsComponent implements OnInit {

  book$!: Observable<Book>;

  constructor(private route: ActivatedRoute, private bookService: BookService) { }

  ngOnInit(): void {
    const bookId = this.route.snapshot.paramMap.get('bookId');

    if(bookId === null){
      return;
    }

    this.book$ = this.bookService.getBook(bookId)
  }
}
