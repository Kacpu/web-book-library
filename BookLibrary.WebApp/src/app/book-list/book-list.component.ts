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
    {title: "some title", url: "https://static01.helion.com.pl/global/okladki/326x466/ukrcza.jpg"},
    {title: "some title", url: "https://static01.helion.com.pl/global/okladki/326x466/ukrcza.jpg"},
    {title: "some title lorem impsum asiasissssjjjjhhhaisnianinaisninais", url: "https://static01.helion.com.pl/global/okladki/326x466/ukrcza.jpg"},
    {title: "some title", url: "https://static01.helion.com.pl/global/okladki/326x466/ukrcza.jpg"},
    {title: "some title", url: "https://static01.helion.com.pl/global/okladki/326x466/ukrcza.jpg"},
    {title: "some title", url: "https://static01.helion.com.pl/global/okladki/326x466/ukrcza.jpg"},
    {title: "some title", url: "https://static01.helion.com.pl/global/okladki/326x466/ukrcza.jpg"},
  ]

}
