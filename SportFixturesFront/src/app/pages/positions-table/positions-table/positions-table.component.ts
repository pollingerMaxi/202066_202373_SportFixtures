import { Component, OnInit } from '@angular/core';
import { PositionsTable } from 'src/app/shared/interfaces/positions-table';
import { PositionsService } from 'src/app/services';

@Component({
  selector: 'app-positions-table',
  templateUrl: './positions-table.component.html',
  styleUrls: ['./positions-table.component.css']
})
export class PositionsTableComponent implements OnInit {
  public positions: PositionsTable[];
  public cols: any[];

  constructor(
    private positionsService: PositionsService
  ) { }

  ngOnInit() {
    this.cols = [
      { field: 'position', header: 'Position' },
      { field: 'name', header: 'Name' },
      { field: 'points', header: 'Points' }
    ];
  }

  public getPositions(sportId: string) {
    this.positionsService.getPositions(sportId).then(positions => this.positions = positions);
  }

}
