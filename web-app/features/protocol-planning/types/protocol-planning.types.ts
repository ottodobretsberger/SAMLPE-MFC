export const examTypes = ['General XRay', 'Cardiac CT', 'Chest MRI'] as const;

export type ExamType = (typeof examTypes)[number];

export type ProtocolPlanningScenario = {
  examType: ExamType;
  patientWeightKg: number;
  targetDoseMlPerKg: number;
  scannerCapacityPerHour: number;
};

export type ProtocolPlanningScenarioInput = {
  examType: ExamType;
  patientWeightKg: string;
  targetDoseMlPerKg: string;
  scannerCapacityPerHour: string;
};

export type ProtocolPlanningResult = {
  contrastVolumeMl: number;
  estimatedSlotMinutes: number;
  suggestedPrepMinutes: number;
  operationalNote: string;
  status: 'draft' | 'calculated';
};

export type ProtocolPlanningValidationResult = {
  scenario: ProtocolPlanningScenario | null;
  errors: string[];
};
