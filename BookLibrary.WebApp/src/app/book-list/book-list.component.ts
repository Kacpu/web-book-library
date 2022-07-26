import { Component, OnInit } from '@angular/core';

@Component({
  selector: 'app-book-list',
  templateUrl: './book-list.component.html',
  styleUrls: ['./book-list.component.css']
})
export class BookListComponent implements OnInit {

  constructor() { }

  ngOnInit(): void {
  }

  books = [
    {title: "some title", color: "red"},
    {title: "some title", color: "red"},
    {title: "some title", color: "red"},
    {title: "some title", color: "red"},
    {title: "some title", color: "red"},
    {title: "some title", color: "red"},
    {title: "some title", color: "red"},
    {title: "some title", color: "red"},
    {title: "some title", color: "red"},
    {title: "some title", color: "red"},
    {title: "some title", color: "red"},
    {title: "some titleeeeeeeddddddddddd", color: "red"},
    {title: "some title", color: "red"},
    {title: "some title", color: "red"},
    {title: "some title", color: "red"},
  ]

}
