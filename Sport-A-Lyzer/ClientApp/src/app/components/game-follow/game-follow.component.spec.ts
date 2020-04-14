import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { GameFollowComponent } from './game-follow.component';

describe('GameFollowComponent', () => {
  let component: GameFollowComponent;
  let fixture: ComponentFixture<GameFollowComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ GameFollowComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(GameFollowComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
