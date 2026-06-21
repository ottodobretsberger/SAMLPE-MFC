import type {
  ProtocolPlanningScenario,
  ProtocolPlanningScenarioInput,
  ProtocolPlanningValidationResult,
} from '@/features/protocol-planning/types/protocol-planning.types';

export function validateProtocolPlanningScenario(input: ProtocolPlanningScenario): string[] {
  const errors: string[] = [];

  if (input.patientWeightKg < 10 || input.patientWeightKg > 300) {
    errors.push('Patient weight must be between 10.0 and 300.0 kg.');
  }

  if (input.targetDoseMlPerKg < 0.1 || input.targetDoseMlPerKg > 5.0) {
    errors.push('Target dose must be between 0.1 and 5.0 mL/kg.');
  }

  if (input.scannerCapacityPerHour < 1 || input.scannerCapacityPerHour > 12) {
    errors.push('Scanner capacity must be between 1 and 12 patients/hr.');
  }

  return errors;
}

export function parseProtocolPlanningScenario(input: ProtocolPlanningScenarioInput): ProtocolPlanningValidationResult {
  const scenario: ProtocolPlanningScenario = {
    examType: input.examType,
    patientWeightKg: Number.parseFloat(input.patientWeightKg),
    targetDoseMlPerKg: Number.parseFloat(input.targetDoseMlPerKg),
    scannerCapacityPerHour: Number.parseFloat(input.scannerCapacityPerHour),
  };

  const errors: string[] = [];

  if (Number.isNaN(scenario.patientWeightKg)) {
    errors.push('Patient weight must be a valid number.');
  }

  if (Number.isNaN(scenario.targetDoseMlPerKg)) {
    errors.push('Target dose must be a valid number.');
  }

  if (Number.isNaN(scenario.scannerCapacityPerHour)) {
    errors.push('Scanner capacity must be a valid number.');
  }

  if (errors.length > 0) {
    return {
      scenario: null,
      errors,
    };
  }

  const rangeErrors = validateProtocolPlanningScenario(scenario);

  return {
    scenario: rangeErrors.length === 0 ? scenario : null,
    errors: rangeErrors,
  };
}
