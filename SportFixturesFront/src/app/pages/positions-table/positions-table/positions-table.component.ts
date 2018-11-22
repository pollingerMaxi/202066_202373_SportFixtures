import { Component, OnInit } from '@angular/core';
import { PositionsTable } from 'src/app/shared/interfaces/positions-table';
import { PositionsService, SportService } from 'src/app/services';
import { Sport } from 'src/app/shared/models';

@Component({
  selector: 'app-positions-table',
  templateUrl: './positions-table.component.html',
  styleUrls: ['./positions-table.component.css']
})
export class PositionsTableComponent implements OnInit {
  public positions: PositionsTable[];
  public sports: Sport[];
  public selectedSport: Sport;
  public positionCounter: number = 0;
  public positionForDisplay: any[] = [];

  constructor(
    private positionsService: PositionsService,
    private sportService: SportService) {
    this.selectedSport = new Sport();
  }

  ngOnInit() {
    this.getSports();
  }

  public async getPositions(sportId: string) {
    this.positions = await this.positionsService.getPositions(sportId);
    this.positions.sort(function
      (first, second) {
      return parseInt(second.points) - parseInt(first.points)
    });
    this.positions.forEach(position => {
      this.positionForDisplay[position.team.name] = ++this.positionCounter;
    });
  }

  public async getSports() {
    this.sports = await this.sportService.getSports();
  }

}
