import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { MatchUnityComponent } from './match-unity.component';

describe('MatchUnityComponent', () => {
  let component: MatchUnityComponent;
  let fixture: ComponentFixture<MatchUnityComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ MatchUnityComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(MatchUnityComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
