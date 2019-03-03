/* tslint:disable:no-unused-variable */

import { TestBed, async, inject } from '@angular/core/testing';
import { JudgeService } from './judge.service';

describe('Service: Judge', () => {
  beforeEach(() => {
    TestBed.configureTestingModule({
      providers: [JudgeService]
    });
  });

  it('should ...', inject([JudgeService], (service: JudgeService) => {
    expect(service).toBeTruthy();
  }));
});
