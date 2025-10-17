import { ComponentFixture, TestBed } from '@angular/core/testing';
import { MAT_DIALOG_DATA, MatDialogRef } from '@angular/material/dialog';
import { provideHttpClient } from '@angular/common/http';
import { Delete } from './delete';
import { User } from '../../models/user';

describe('Delete', () => {
  let component: Delete;
  let fixture: ComponentFixture<Delete>;

  const mockUser: User = {
    id: 1,
    name: 'Test',
    lastName: 'User',
    department: 'TI',
    shift: 'Manhã',
    active: true
  };

  const mockDialogRef = {
    close: jasmine.createSpy('close')
  };

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [Delete],
      providers: [
        provideHttpClient(),
        { provide: MAT_DIALOG_DATA, useValue: mockUser },
        { provide: MatDialogRef, useValue: mockDialogRef }
      ]
    })
    .compileComponents();

    fixture = TestBed.createComponent(Delete);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
