import { ComponentFixture, TestBed } from '@angular/core/testing';

import { CupUnityComponent } from './cup-unity.component';

describe('CupUnityComponent', () => {
  let component: CupUnityComponent;
  let fixture: ComponentFixture<CupUnityComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ CupUnityComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(CupUnityComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
