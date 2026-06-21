'use client';

import { useState } from 'react';
import { parseProtocolPlanningScenario } from '@/features/protocol-planning/schemas/protocol-planning.schema';
import { examTypes, type ProtocolPlanningScenarioInput } from '@/features/protocol-planning/types/protocol-planning.types';
import { calculateProtocolPlan, defaultProtocolPlanningScenario } from '@/features/protocol-planning/utils/calculate-protocol-plan';

const initialScenarioInput: ProtocolPlanningScenarioInput = {
  examType: defaultProtocolPlanningScenario.examType,
  patientWeightKg: defaultProtocolPlanningScenario.patientWeightKg.toFixed(1),
  targetDoseMlPerKg: defaultProtocolPlanningScenario.targetDoseMlPerKg.toFixed(2),
  scannerCapacityPerHour: defaultProtocolPlanningScenario.scannerCapacityPerHour.toString(),
};

export function ProtocolPlanningPage() {
  const [scenarioInput, setScenarioInput] = useState<ProtocolPlanningScenarioInput>(initialScenarioInput);
  const [errors, setErrors] = useState<string[]>([]);
  const [result, setResult] = useState(() => calculateProtocolPlan(defaultProtocolPlanningScenario));
  const [lastCalculatedSummary, setLastCalculatedSummary] = useState(
    `${defaultProtocolPlanningScenario.examType} · ${defaultProtocolPlanningScenario.patientWeightKg.toFixed(1)} kg · ${defaultProtocolPlanningScenario.targetDoseMlPerKg.toFixed(2)} mL/kg · ${defaultProtocolPlanningScenario.scannerCapacityPerHour} patients/hr`,
  );

  function handleFieldChange(field: keyof ProtocolPlanningScenarioInput, value: string) {
    setScenarioInput((current) => ({
      ...current,
      [field]: value,
    }));
  }

  function handleCalculate(event: React.FormEvent<HTMLFormElement>) {
    event.preventDefault();

    const validation = parseProtocolPlanningScenario(scenarioInput);
    setErrors(validation.errors);

    if (!validation.scenario) {
      return;
    }

    setResult(calculateProtocolPlan(validation.scenario));
    setLastCalculatedSummary(
      `${validation.scenario.examType} · ${validation.scenario.patientWeightKg.toFixed(1)} kg · ${validation.scenario.targetDoseMlPerKg.toFixed(2)} mL/kg · ${validation.scenario.scannerCapacityPerHour} patients/hr`,
    );
  }

  function handleReset() {
    setScenarioInput(initialScenarioInput);
    setErrors([]);
    setResult(calculateProtocolPlan(defaultProtocolPlanningScenario));
    setLastCalculatedSummary(
      `${defaultProtocolPlanningScenario.examType} · ${defaultProtocolPlanningScenario.patientWeightKg.toFixed(1)} kg · ${defaultProtocolPlanningScenario.targetDoseMlPerKg.toFixed(2)} mL/kg · ${defaultProtocolPlanningScenario.scannerCapacityPerHour} patients/hr`,
    );
  }

  return (
    <main style={{ padding: '40px 32px 64px' }}>
      <div style={{ maxWidth: '1200px', margin: '0 auto' }}>
        <section
          style={{
            background: 'linear-gradient(135deg, #0f5ea8 0%, #1a7fd1 100%)',
            color: '#ffffff',
            borderRadius: 'var(--radius-xl)',
            boxShadow: 'var(--shadow-lg)',
            padding: '40px',
            marginBottom: '24px',
          }}
        >
          <p style={{ margin: 0, opacity: 0.85, fontWeight: 600 }}>Pilot Workflow</p>
          <h1 style={{ margin: '12px 0 10px', fontSize: 'var(--font-size-2xl)' }}>Protocol Planning Workbench</h1>
          <p style={{ margin: 0, maxWidth: '760px', lineHeight: 1.6 }}>
            Plan contrast volume, slot duration, and prep timing for imaging workflows with a modern web-first
            decision-support experience.
          </p>
        </section>

        <div
          style={{
            display: 'grid',
            gridTemplateColumns: 'minmax(320px, 420px) 1fr',
            gap: '24px',
            alignItems: 'start',
          }}
        >
          <section
            style={{
              background: 'var(--color-surface)',
              border: '1px solid var(--color-border)',
              borderRadius: 'var(--radius-lg)',
              boxShadow: 'var(--shadow-sm)',
              padding: '24px',
            }}
          >
            <h2 style={{ marginTop: 0 }}>Scenario Inputs</h2>
            <p style={{ color: 'var(--color-text-muted)', lineHeight: 1.6 }}>
              Adjust the scenario, then calculate the operational recommendation using the same planning rules defined in
              the MFC and WPF demos.
            </p>

            <form onSubmit={handleCalculate} style={{ display: 'grid', gap: '18px', marginTop: '24px' }}>
              <Field label="Exam type" helper="Select the imaging protocol category for this planning scenario.">
                <select
                  value={scenarioInput.examType}
                  onChange={(event) => handleFieldChange('examType', event.target.value)}
                  style={inputStyle}
                >
                  {examTypes.map((examType) => (
                    <option key={examType} value={examType}>
                      {examType}
                    </option>
                  ))}
                </select>
              </Field>

              <Field label="Patient weight (kg)" helper="Allowed range: 10.0 to 300.0 kg.">
                <input
                  value={scenarioInput.patientWeightKg}
                  onChange={(event) => handleFieldChange('patientWeightKg', event.target.value)}
                  style={inputStyle}
                  inputMode="decimal"
                />
              </Field>

              <Field label="Target dose (mL/kg)" helper="Allowed range: 0.1 to 5.0 mL/kg.">
                <input
                  value={scenarioInput.targetDoseMlPerKg}
                  onChange={(event) => handleFieldChange('targetDoseMlPerKg', event.target.value)}
                  style={inputStyle}
                  inputMode="decimal"
                />
              </Field>

              <Field label="Scanner capacity (patients/hr)" helper="Allowed range: 1 to 12 patients/hr.">
                <input
                  value={scenarioInput.scannerCapacityPerHour}
                  onChange={(event) => handleFieldChange('scannerCapacityPerHour', event.target.value)}
                  style={inputStyle}
                  inputMode="numeric"
                />
              </Field>

              {errors.length > 0 ? (
                <div
                  style={{
                    background: '#fff3f2',
                    border: '1px solid #f3c3bf',
                    borderRadius: 'var(--radius-md)',
                    color: 'var(--color-danger)',
                    padding: '16px',
                  }}
                >
                  <p style={{ margin: '0 0 8px', fontWeight: 700 }}>Please correct the following:</p>
                  <ul style={{ margin: 0, paddingLeft: '20px' }}>
                    {errors.map((error) => (
                      <li key={error}>{error}</li>
                    ))}
                  </ul>
                </div>
              ) : null}

              <div style={{ display: 'flex', gap: '12px', flexWrap: 'wrap' }}>
                <button type="submit" style={primaryButtonStyle}>
                  Calculate plan
                </button>
                <button type="button" onClick={handleReset} style={secondaryButtonStyle}>
                  Reset defaults
                </button>
              </div>
            </form>
          </section>

          <section style={{ display: 'grid', gap: '24px' }}>
            <div
              style={{
                background: 'var(--color-surface)',
                border: '1px solid var(--color-border)',
                borderRadius: 'var(--radius-lg)',
                boxShadow: 'var(--shadow-sm)',
                padding: '20px 24px',
              }}
            >
              <p style={{ margin: 0, color: 'var(--color-text-muted)', fontSize: 'var(--font-size-sm)' }}>Last calculated scenario</p>
              <p style={{ margin: '10px 0 0', fontWeight: 600 }}>{lastCalculatedSummary}</p>
            </div>

            <div
              style={{
                display: 'grid',
                gridTemplateColumns: 'repeat(3, minmax(0, 1fr))',
                gap: '16px',
              }}
            >
              <MetricCard label="Calculated Contrast Volume" value={`${result.contrastVolumeMl.toFixed(1)} mL`} />
              <MetricCard label="Estimated Slot Duration" value={`${result.estimatedSlotMinutes} min`} />
              <MetricCard label="Suggested Prep Buffer" value={`${result.suggestedPrepMinutes} min`} />
            </div>

            <div
              style={{
                background: 'var(--color-surface)',
                border: '1px solid var(--color-border)',
                borderRadius: 'var(--radius-lg)',
                boxShadow: 'var(--shadow-sm)',
                padding: '24px',
              }}
            >
              <h2 style={{ marginTop: 0 }}>Operational Guidance</h2>
              <p style={{ color: 'var(--color-text-muted)', lineHeight: 1.7 }}>{result.operationalNote}</p>
              <p style={{ color: 'var(--color-text-muted)', lineHeight: 1.7, marginBottom: 0 }}>
                Use the slot duration and prep buffer together when evaluating schedule density, staffing alignment, and
                throughput pressure for the selected exam type.
              </p>
            </div>
          </section>
        </div>
      </div>
    </main>
  );
}

function Field({
  label,
  helper,
  children,
}: {
  label: string;
  helper: string;
  children: React.ReactNode;
}) {
  return (
    <label style={{ display: 'grid', gap: '8px' }}>
      <span style={{ fontWeight: 600 }}>{label}</span>
      {children}
      <span style={{ color: 'var(--color-text-muted)', fontSize: 'var(--font-size-sm)', lineHeight: 1.5 }}>{helper}</span>
    </label>
  );
}

function MetricCard({ label, value }: { label: string; value: string }) {
  return (
    <article
      style={{
        background: 'var(--color-surface)',
        border: '1px solid var(--color-border)',
        borderRadius: 'var(--radius-lg)',
        boxShadow: 'var(--shadow-sm)',
        padding: '20px',
      }}
    >
      <p style={{ margin: 0, color: 'var(--color-text-muted)', fontSize: 'var(--font-size-sm)' }}>{label}</p>
      <p style={{ margin: '10px 0 0', fontWeight: 700, fontSize: 'var(--font-size-xl)' }}>{value}</p>
    </article>
  );
}

const inputStyle: React.CSSProperties = {
  width: '100%',
  minHeight: '46px',
  padding: '0 14px',
  borderRadius: 'var(--radius-md)',
  border: '1px solid var(--color-border)',
  background: '#ffffff',
  color: 'var(--color-text)',
  fontSize: 'var(--font-size-md)',
};

const primaryButtonStyle: React.CSSProperties = {
  minHeight: '46px',
  border: 'none',
  borderRadius: '999px',
  padding: '0 18px',
  background: 'var(--color-primary)',
  color: 'var(--color-primary-foreground)',
  fontWeight: 700,
  cursor: 'pointer',
};

const secondaryButtonStyle: React.CSSProperties = {
  minHeight: '46px',
  borderRadius: '999px',
  padding: '0 18px',
  border: '1px solid var(--color-border)',
  background: 'var(--color-surface)',
  color: 'var(--color-text)',
  fontWeight: 700,
  cursor: 'pointer',
};
