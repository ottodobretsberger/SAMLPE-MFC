import type { ProtocolPlanningResult, ProtocolPlanningScenario } from '@/features/protocol-planning/types/protocol-planning.types';

export const defaultProtocolPlanningScenario: ProtocolPlanningScenario = {
  examType: 'General XRay',
  patientWeightKg: 75.0,
  targetDoseMlPerKg: 1.2,
  scannerCapacityPerHour: 3,
};

export function calculateProtocolPlan(scenario: ProtocolPlanningScenario): ProtocolPlanningResult {
  const contrastVolumeMl = roundToOneDecimal(scenario.patientWeightKg * scenario.targetDoseMlPerKg);
  const estimatedSlotMinutes = Math.round(60 / scenario.scannerCapacityPerHour);
  const prepFactor =
    scenario.examType === 'Cardiac CT' ? 1.15 : scenario.examType === 'Chest MRI' ? 1.1 : 1.0;
  const suggestedPrepMinutes = Math.round(estimatedSlotMinutes * prepFactor);

  return {
    contrastVolumeMl,
    estimatedSlotMinutes,
    suggestedPrepMinutes,
    operationalNote: 'Use this estimate for scheduling triage and staffing alignment.',
    status: 'calculated',
  };
}

function roundToOneDecimal(value: number) {
  return Math.round(value * 10) / 10;
}
