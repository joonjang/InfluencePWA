import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { QuoteDataComponent } from './quote-data.component';

describe('QuoteDataComponent', () => {
  let component: QuoteDataComponent;
  let fixture: ComponentFixture<QuoteDataComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ QuoteDataComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(QuoteDataComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
