import { TestBed } from '@angular/core/testing';

import { URLNormalizerService } from './urlnormalizer.service';

describe('URLNormalizerService', () => {
  let service: URLNormalizerService;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(URLNormalizerService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
