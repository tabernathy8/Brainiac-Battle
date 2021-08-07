import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { GardenUnityComponent } from './garden-unity.component';

describe('GardenUnityComponent', () => {
  let component: GardenUnityComponent;
  let fixture: ComponentFixture<GardenUnityComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ GardenUnityComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(GardenUnityComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
