import { ComponentFixture, TestBed } from '@angular/core/testing';

import { AutocompleteSearchBarComponent } from './autocomplete-search-bar.component';

describe('AutocompleteSearchBarComponent', () => {
  let component: AutocompleteSearchBarComponent;
  let fixture: ComponentFixture<AutocompleteSearchBarComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ AutocompleteSearchBarComponent ]
    })
    .compileComponents();

    fixture = TestBed.createComponent(AutocompleteSearchBarComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
