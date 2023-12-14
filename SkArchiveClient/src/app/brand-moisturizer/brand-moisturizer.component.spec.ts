import { ComponentFixture, TestBed } from '@angular/core/testing';

import { BrandMoisturizerComponent } from './brand-moisturizer.component';

describe('BrandMoisturizerComponent', () => {
  let component: BrandMoisturizerComponent;
  let fixture: ComponentFixture<BrandMoisturizerComponent>;

  beforeEach(() => {
    TestBed.configureTestingModule({
      declarations: [BrandMoisturizerComponent]
    });
    fixture = TestBed.createComponent(BrandMoisturizerComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
