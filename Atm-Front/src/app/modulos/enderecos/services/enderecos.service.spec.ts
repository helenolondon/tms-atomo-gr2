import { TestBed } from '@angular/core/testing';

import { EnderecosService } from './enderecos.service';

describe('EnderecosService', () => {
  beforeEach(() => TestBed.configureTestingModule({}));

  it('should be created', () => {
    const service: EnderecosService = TestBed.get(EnderecosService);
    expect(service).toBeTruthy();
  });
});
