import { ComponentFixture, TestBed } from '@angular/core/testing';

import { ModalPlatosComponent } from './modal-platos.component';

describe('ModalPlatosComponent', () => {
  let component: ModalPlatosComponent;
  let fixture: ComponentFixture<ModalPlatosComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ ModalPlatosComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(ModalPlatosComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
