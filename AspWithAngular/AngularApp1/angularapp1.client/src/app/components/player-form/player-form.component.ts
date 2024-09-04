import { Component, OnInit, Input } from '@angular/core';
import { PlayerService, Player } from '../../services/player.service';
import { Router } from '@angular/router';
import { ToastrService } from 'ngx-toastr';

@Component({
  selector: 'app-player-form',
  templateUrl: './player-form.component.html',
  styleUrls: ['./player-form.component.css']
})
export class PlayerFormComponent implements OnInit {
  @Input() player: Player = { id: 0, shirtNo: 0, name: '', positionId: 0, appearances: 0, nationality: '' };
  positions: any[] = [];
  isEditMode: boolean = false;

  constructor(private playerService: PlayerService, private router: Router, private toastr: ToastrService) { }

  ngOnInit(): void {
    // Initialize positions and check if in edit mode
    // this.positions = ... fetch from a service
    this.isEditMode = this.player.id > 0;
  }

  onSubmit(): void {
    if (this.isEditMode) {
      this.playerService.updatePlayer(this.player).subscribe(() => {
        this.toastr.success('Player updated successfully');
        this.router.navigate(['/']);
      });
    } else {
      this.playerService.addPlayer(this.player).subscribe(() => {
        this.toastr.success('Player added successfully');
        this.router.navigate(['/']);
      });
    }
  }
  cancel(): void {
    this.router.navigate(['/']);
  }
}
