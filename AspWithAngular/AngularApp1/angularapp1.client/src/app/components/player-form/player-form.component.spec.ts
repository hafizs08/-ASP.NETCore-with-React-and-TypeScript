import { ComponentFixture, TestBed } from '@angular/core/testing';

import { PlayerFormComponent } from './player-form.component';

describe('PlayerFormComponent', () => {
  ngOnInit(): void {
    this.loadPositions();
  }

  loadPositions(): void {
    this.playerService.getPositions().subscribe(response => {
      console.log(response); // Debug response to check the structure
      this.positions = Array.isArray(response) ? response : []; // Ensure positions is an array
    });
  }
  let component: PlayerFormComponent;
  let fixture: ComponentFixture<PlayerFormComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [PlayerFormComponent]
    })
    .compileComponents();

    fixture = TestBed.createComponent(PlayerFormComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });


  it('should create', () => {
    expect(component).toBeTruthy();
  });
});


