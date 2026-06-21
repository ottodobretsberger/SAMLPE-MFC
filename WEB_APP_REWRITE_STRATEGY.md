# Web App Rewrite Strategy

## Purpose
This document captures the decisions made so far for the next rewrite of the demo healthcare application, along with the target architecture, design direction, delivery approach, and a place to record future findings and decisions.

This is intended to be a living document for reproducibility and project continuity.

## Current Situation

### Existing applications
- `MFCSDIAppln/` contains the original MFC demo application.
- `WpfSDIAppln/` contains a WPF rewrite of the same app.

### Why the WPF rewrite exists
The WPF version preserves the familiar desktop-style experience for demo scenarios where visual continuity matters, including:
- training purposes
- training materials
- customers who want a familiar transition path

### New goal
Create a separate web rewrite of the same application that does **not** preserve the old visual style.

The web version should feel:
- modern
- premium
- polished
- current
- visually impressive
- easy to use

The intent is for users to react with "wow" because the application feels like a modern product, not a browser copy of an old desktop screen.

---

## Decision Summary

### Primary frontend recommendation
Use **React with Next.js**.

### Recommended stack
- **Next.js**
- **React**
- **TypeScript**
- **Tailwind CSS**
- **shadcn/ui**
- **Framer Motion**
- **TanStack Query**
- **Zustand**
- **React Hook Form**
- **Zod**

### Why this stack
This combination is the best fit for a high-quality healthcare web application because it offers:
- a mature and widely adopted ecosystem
- strong long-term maintainability
- fast delivery of polished interfaces
- excellent support for forms, workflows, and dashboards
- strong accessibility support
- a good balance between developer speed and enterprise readiness

---

## Architecture Decision

### Chosen direction
Build the web rewrite as a **task-oriented, design-forward web application** rather than a screen-by-screen desktop clone.

### Explicit non-goal
Do **not** recreate the MFC or WPF UI literally in HTML.

### UX principle
Preserve business intent and workflows, but redesign the experience for the web.

That means:
- keep the important use cases
- simplify flows where possible
- modernize navigation
- improve visual hierarchy
- use responsive layouts
- reduce desktop-era UI clutter

---

## Why Next.js over the main alternatives

### Versus Angular
Angular is strong for enterprise applications, but for this project it is less attractive because:
- UI customization is generally slower
- creating a highly polished, premium visual design takes more effort
- the React ecosystem is better suited to rapid modern UI iteration

### Versus Vue
Vue is a good option, but for this project React/Next.js is preferred because:
- it has a broader enterprise talent pool
- it has stronger ecosystem depth for design systems and healthcare-style internal apps
- it is a safer long-term default for a business-facing rewrite

### Final recommendation
If one framework must be chosen today, choose **Next.js**.

---

## Product and UX Goals

The new web application should aim for the following user perception:
- trustworthy
- efficient
- modern
- premium
- clean
- calm
- easy to learn

### Target product feeling
The correct tone is:
- modern enterprise healthcare platform

The wrong tone would be:
- old hospital software in a browser
- a nostalgic desktop clone
- an over-animated consumer app

### What creates the "wow"
The wow factor should come from:
- strong UX redesign
- excellent layout and spacing
- great typography
- modern component styling
- smooth and subtle motion
- clear task-focused flows
- polished empty, loading, and success states

The wow factor should **not** come from:
- excessive animation
- flashy but confusing visuals
- unnecessary gradients and visual noise
- trying to mimic old desktop controls in a browser

---

## Known Application Areas from the Current Rewrite

Based on the current MFC/WPF implementations and available documentation, the known major UI areas are:
- Main view
- Form view
- Edit view
- Toolbar actions
- Menu-driven document actions
- Status area / state indicators

These are useful as migration inputs, but they should not define the final web structure one-for-one.

---

## Recommended Web Information Architecture

Instead of modeling the app around desktop views, organize it around user tasks and business areas.

### Proposed top-level app sections
- Dashboard
- Patients
- Scheduling
- Encounters or Workflows
- Documents / Notes
- Reports
- Administration
- Settings

### Proposed shell layout
Use a modern application shell with:
- left navigation
- top header or command bar
- page title and breadcrumb area
- central content region
- contextual side panel or drawer for secondary actions

This structure will feel more natural on the web than a desktop-inspired menu-and-toolbar-first layout.

---

## Recommended Frontend Blueprint

### Core architectural principles
1. Use **feature-based organization** rather than organizing primarily by technical layers.
2. Separate **UI components**, **domain logic**, **server state**, and **form logic**.
3. Keep API calls out of presentation-only components.
4. Reuse a small, intentional design system across all modules.
5. Prefer a composable shell and reusable workflow patterns.

### Suggested folder structure

```text
web-app/
  app/
	(dashboard)/
	patients/
	scheduling/
	documents/
	reports/
	admin/
	settings/
	layout.tsx
	page.tsx

  components/
	app-shell/
	data-display/
	forms/
	navigation/
	feedback/
	overlays/

  features/
	patients/
	  api/
	  components/
	  hooks/
	  schemas/
	  types/
	scheduling/
	  api/
	  components/
	  hooks/
	  schemas/
	  types/
	documents/
	  api/
	  components/
	  hooks/
	  schemas/
	  types/

  lib/
	api/
	auth/
	config/
	format/
	utils/

  store/
	ui/

  styles/
	globals.css
	tokens.css

  types/

  public/
```

### What belongs where
- `app/`: routes, layouts, page entry points
- `features/`: domain-specific behavior and UI
- `components/`: shared reusable presentational building blocks
- `lib/api/`: base client, interceptors, typed request helpers
- `store/ui/`: lightweight client-side UI state
- `styles/`: global styles and design tokens

## Concrete Next.js Project Blueprint

This section turns the strategy into a practical implementation blueprint for the future web application.

### Runtime and platform assumptions
- Use the **Next.js App Router**.
- Use **TypeScript** everywhere.
- Prefer **server components by default**.
- Use **client components only when needed** for interactivity, browser APIs, animation boundaries, or form handling.
- Treat the web app as a **separate frontend application** in this repository or in a sibling delivery repository.

### Recommended project root
If the web app is added inside this repository, create it under:
- `web-app/`

### Concrete folder structure

```text
web-app/
  app/
    (app)/
      dashboard/
        page.tsx
      patients/
        page.tsx
        [patientId]/
          page.tsx
      scheduling/
        page.tsx
      documents/
        page.tsx
        [documentId]/
          page.tsx
      reports/
        page.tsx
      admin/
        page.tsx
      settings/
        page.tsx
      layout.tsx
    (marketing)/
      page.tsx
    api/
      health/
        route.ts
    favicon.ico
    globals.css
    layout.tsx
    page.tsx
    providers.tsx

  components/
    app-shell/
      app-shell.tsx
      sidebar-nav.tsx
      top-bar.tsx
      breadcrumb-header.tsx
    data-display/
      data-table.tsx
      stat-card.tsx
      status-badge.tsx
      empty-state.tsx
      skeleton-loader.tsx
    forms/
      form-field.tsx
      form-section.tsx
      search-input.tsx
      filter-bar.tsx
    feedback/
      alert-banner.tsx
      toast-region.tsx
      inline-status.tsx
    overlays/
      drawer.tsx
      modal.tsx
      command-palette.tsx
    navigation/
      tabs.tsx
      page-header.tsx
      section-header.tsx
      context-header.tsx

  features/
    dashboard/
      components/
      hooks/
      types/
    patients/
      api/
        get-patients.ts
        get-patient-by-id.ts
      components/
      hooks/
      schemas/
      types/
      utils/
    scheduling/
      api/
      components/
      hooks/
      schemas/
      types/
    documents/
      api/
      components/
      hooks/
      schemas/
      types/
      utils/
    reports/
      api/
      components/
      hooks/
      types/

  lib/
    api/
      client.ts
      errors.ts
      types.ts
    auth/
      session.ts
      permissions.ts
    config/
      env.ts
      navigation.ts
    format/
      dates.ts
      names.ts
      status.ts
    utils/
      cn.ts
      assert.ts
      download.ts

  store/
    ui/
      app-shell-store.ts
      command-palette-store.ts
      filters-store.ts

  styles/
    tokens.css
    theme.css

  types/
    api.ts
    common.ts
    ui.ts

  public/
    images/
    icons/
```

### Route design
Use route groups to separate the authenticated app shell from public or marketing pages.

#### Primary route map
- `/` - landing or entry redirect
- `/dashboard` - high-level workspace overview
- `/patients` - patient search and listing
- `/patients/[patientId]` - patient detail workspace
- `/scheduling` - scheduling dashboard and actions
- `/documents` - document and note list
- `/documents/[documentId]` - document workspace
- `/reports` - reporting surfaces
- `/admin` - administration tools
- `/settings` - preferences and configuration

#### Routing rules
- Use **resource-oriented URLs**.
- Use nested routes for detail workspaces.
- Keep filter state in the URL when it is meaningful for sharing or refresh persistence.
- Use route segments for major workflows, not for every small UI state.

### App shell blueprint
The authenticated shell should be defined in `app/(app)/layout.tsx` and include:
- left sidebar navigation
- top bar with global search and quick actions
- breadcrumb or page context header
- main content region
- right-side contextual drawer where needed
- toast region and modal host

### Provider composition
Create `app/providers.tsx` and keep global providers there.

#### Initial providers
- `QueryClientProvider`
- theme provider if needed
- toast provider
- command palette provider if the implementation requires one

#### Provider rule
Do not wrap the entire application in unnecessary client-only providers if the functionality can remain server-rendered.

### Server component and client component rules

#### Default rule
Use server components for:
- layout composition
- page data loading where server rendering makes sense
- static or semi-static sections
- initial route payload assembly

#### Use client components for:
- forms
- interactive filters
- drawers and modals
- command palette interactions
- animated UI sections
- browser-only integrations

#### Avoid
- marking entire pages as client components without clear need
- duplicating fetches in both server and client layers unless the pattern requires hydration

---

## State Management Rules

### Server state
Use **TanStack Query** for:
- data fetching
- caching
- background refresh
- optimistic updates where appropriate
- mutation states

### Client UI state
Use **Zustand** only for small, shared UI concerns such as:
- side panel open state
- command palette state
- selected filters that are not route-driven
- temporary client session preferences

### Form state
Use **React Hook Form** with **Zod** for:
- form value management
- validation
- reusable schemas
- step-based workflows

### Rule of thumb
- If data comes from the server, treat it as server state.
- If it only controls UI behavior, treat it as UI state.
- If it belongs to an input workflow, keep it in form state.

Avoid putting everything into a single global store.

### Query key convention
Use stable, explicit query keys.

Examples:
- `['patients', 'list', filters]`
- `['patients', patientId]`
- `['documents', 'list', filters]`
- `['documents', documentId]`

### Mutation handling rules
- Keep mutations close to the feature that owns them.
- Invalidate only the minimum required query keys.
- Prefer optimistic updates only where user value is high and rollback is simple.
- Surface mutation status in the UI with clear feedback.

### URL state rules
Put the following in the URL when useful:
- search terms
- sort order
- pagination state
- selected high-level filters
- current tab if it affects shareable context

Keep the following out of the URL unless there is a strong reason:
- drawer open state
- transient hover state
- temporary draft UI toggles

---

## API Integration Rules

### Recommended approach
Create a typed API layer that is isolated from the UI.

### Rules
- No direct fetch logic inside page-level visual components.
- Each feature should own its own API functions.
- Shared request behavior should live in a central client.
- Request and response types should be explicit.
- Validation should happen at boundaries where practical.

### Example structure
- `features/patients/api/` for patient-specific requests
- `features/documents/api/` for document-specific requests
- `lib/api/client.ts` for common transport behavior

### API client blueprint
The shared API client should handle:
- base URL configuration
- auth header injection
- standardized error parsing
- request timeout behavior
- JSON serialization defaults
- correlation or request identifiers if needed later

### DTO and domain model rule
If backend payloads are messy or unstable:
- keep raw DTOs in `api/`
- map them into cleaner UI-facing types in feature utilities
- avoid spreading backend naming inconsistencies throughout the UI

### Validation boundary rule
Use Zod for:
- form validation
- parsing externally sourced data where appropriate
- protecting critical workflow assumptions at the boundary

### Error handling model
Handle errors at three levels:
1. **transport error** - network or server availability problems
2. **feature error** - invalid operation for the current workflow
3. **validation error** - user input or schema mismatch

Each level should produce user feedback that is clear and non-technical where possible.

### Naming conventions
- File names: kebab-case for shared frontend files
- React components: PascalCase exports
- Hooks: `useXyz`
- Query helpers: verb-based names such as `get-patient-by-id`
- Stores: `<scope>-store.ts`
- Validation schemas: `<feature>.schema.ts` or feature-specific schema files under `schemas/`

### Import rules
- Prefer feature-local imports inside a feature.
- Shared concerns should come from `components/`, `lib/`, `store/`, `styles/`, or `types/`.
- Avoid cross-feature imports unless the dependency is truly shared and stable.

---

## Design System Foundation

### Design direction
The web application should look:
- premium
- calm
- precise
- modern
- clean
- healthcare-appropriate

### Color direction
Use a restrained healthcare-friendly palette.

#### Recommended palette concept
- **Primary**: deep blue or blue-teal
- **Secondary accent**: emerald or cyan
- **Neutral base**: slate, gray, or warm gray
- **Status colors**: clear semantic success, warning, error, and info colors

#### Avoid
- overly playful colors
- excessive neon accents
- dated Windows-style gradients
- visual clutter

### Typography
Recommended primary typeface:
- **Inter**

Alternative options:
- Manrope
- Plus Jakarta Sans

### Spacing system
Use an **8px spacing system** consistently.

### Shape and elevation
- medium-soft corner radius
- light shadows
- clean surfaces
- limited border use
- avoid heavy skeuomorphic styling

### Motion
Use **Framer Motion** for:
- page transitions
- drawer transitions
- subtle hover emphasis
- lightweight feedback animations

Motion should be:
- fast
- subtle
- purposeful
- optional where reduced motion is requested

### Suggested design tokens
Start with a small, controlled token system.

#### Color tokens
- `--color-bg`
- `--color-surface`
- `--color-surface-muted`
- `--color-border`
- `--color-text`
- `--color-text-muted`
- `--color-primary`
- `--color-primary-foreground`
- `--color-success`
- `--color-warning`
- `--color-danger`
- `--color-info`

#### Radius tokens
- `--radius-sm`
- `--radius-md`
- `--radius-lg`
- `--radius-xl`

#### Shadow tokens
- `--shadow-sm`
- `--shadow-md`
- `--shadow-lg`

#### Spacing tokens
Use 8px-based spacing values such as:
- `--space-1: 4px`
- `--space-2: 8px`
- `--space-3: 12px`
- `--space-4: 16px`
- `--space-5: 24px`
- `--space-6: 32px`
- `--space-7: 40px`
- `--space-8: 48px`

#### Typography tokens
- `--font-sans`
- `--font-size-xs`
- `--font-size-sm`
- `--font-size-md`
- `--font-size-lg`
- `--font-size-xl`
- `--font-size-2xl`
- `--font-weight-regular`
- `--font-weight-medium`
- `--font-weight-semibold`
- `--font-weight-bold`

### Theme guidance
The initial release should support:
- a polished light theme first
- optional dark theme only if it can be executed well

For demo quality, a strong light theme is more important than shipping both themes too early.

---

## First Reusable Component Set

The initial design system should include:
- `AppShell`
- `SidebarNav`
- `TopBar`
- `PageHeader`
- `SectionHeader`
- `DataTable`
- `FilterBar`
- `SearchInput`
- `CommandPalette`
- `StatCard`
- `StatusBadge`
- `EmptyState`
- `SkeletonLoader`
- `Drawer`
- `Modal`
- `Tabs`
- `FormField`
- `Toast`
- `Alert`
- `ContextHeader`
- `Timeline`

These components should be reusable across modules instead of being recreated per page.

### Component implementation standards
- Prefer composition over large all-in-one components.
- Keep presentational components free of data fetching.
- Expose clear props with stable naming.
- Build empty, loading, disabled, error, and success states early.
- Support keyboard interaction as a first-class concern.
- Keep visual variants intentional and limited.

### Table strategy
Because enterprise healthcare apps often depend on data-heavy views:
- define one shared table pattern early
- standardize sorting, filtering, pagination, selection, and empty states
- avoid each feature inventing its own table behavior

### Form strategy
All important forms should share:
- section layout rules
- field label and help text patterns
- inline validation style
- required indicator behavior
- save and cancel action placement

---

## Accessibility and Healthcare Requirements

Accessibility should be built in from the start.

### Non-negotiable standards
- strong color contrast
- full keyboard navigation
- visible focus states
- reduced-motion support
- screen-reader-friendly forms
- clear validation messages
- large enough click and touch targets
- consistent language and interaction patterns

A healthcare application can look impressive and still remain calm, usable, and accessible.

---

## Modernization Strategy: Desktop to Web

### Principle
Translate the business workflows, not the old desktop widgets.

### Mapping direction

| Current concept | Web reinterpretation |
|---|---|
| Menu bar | Page actions, command palette, contextual actions |
| Toolbar | Top command bar, filters, quick actions |
| Multiple desktop views | Route-based pages and task-focused workspaces |
| Status bar | Inline status, toasts, banners, saved state indicators |
| Edit view | Rich text or structured content workspace |
| Form view | Modern form flows with validation and guidance |
| Main view | Dashboard or home workspace |

### Web-first behaviors to add
- responsive layouts
- smart search
- sticky filters
- side panels instead of popup overload
- better inline validation
- autosave where appropriate
- progressive disclosure for advanced options

---

## Suggested Delivery Phases

### Phase 0 - Discovery
- inventory MFC and WPF screens
- identify true business workflows
- identify data entities and integrations
- capture navigation pain points and redesign opportunities

### Phase 1 - Foundation
- scaffold Next.js application
- add TypeScript, Tailwind, shadcn/ui, Framer Motion
- define design tokens
- build the app shell
- set up API client and state patterns

### Phase 2 - Design System
- build shared components
- define interaction patterns
- establish page templates
- create loading, empty, and error states

### Phase 3 - Workflow Pilot
- choose one high-value workflow
- redesign it for web usage
- validate it with demo stakeholders
- refine patterns before scaling further

### Phase 4 - Core Migration
- implement priority modules
- integrate APIs or mock services
- refine accessibility and responsiveness

### Phase 5 - Demo Readiness
- polish motion and visual quality
- verify edge states
- prepare demo scripts
- validate the "wow" factor in realistic scenarios

### Phase 6 - Hardening
- testing
- performance review
- analytics and telemetry
- documentation updates

---

## Recommended First Workflow to Rebuild

For the web rewrite, the first rebuilt area should be a workflow that best demonstrates:
- visual transformation
- usability improvement
- business relevance
- repeatable architecture patterns

### Selected pilot workflow
Use the **Protocol Planning Workbench** as the first pilot workflow.

### Why this workflow was selected
The current desktop implementations already define a compact but meaningful flow with:
- exam type selection
- patient weight entry
- target dose entry
- scanner capacity entry
- calculation action
- reset action
- computed operational output

This makes it the best first pilot because it can show:
- a dramatic UI modernization from desktop form to premium web workflow
- a clear before-and-after demo story
- strong reuse of form, card, status, and summary components
- a safe first vertical slice with limited backend complexity
- a natural path to add scheduling, staffing, and reporting insights later

### Why the structured report editor is not first
The structured imaging report editor is still an important follow-on workflow, but it is a better second milestone because:
- document editing UX benefits from decisions about autosave, collaboration, and content structure
- it may require richer editing choices than the current demo implies
- the protocol planning workflow is faster to validate and easier to present as a modern web transformation

## Pilot Workflow Blueprint: Protocol Planning Workbench

### Workflow objective
Enable a user to enter a small set of imaging planning inputs and immediately receive a polished operational recommendation for scheduling and preparation.

### Current desktop behavior being preserved logically
The current MFC and WPF demos already support:
- exam type values of `General XRay`, `Cardiac CT`, and `Chest MRI`
- patient weight input
- target dose input
- scanner capacity input
- calculation of contrast volume
- calculation of estimated slot duration
- calculation of suggested prep buffer
- reset to defaults

### Default values from the current implementation
- Exam type: `General XRay`
- Patient weight: `75.0 kg`
- Target dose: `1.20 mL/kg`
- Scanner capacity: `3 patients/hr`

### Current calculation logic to preserve initially
- `contrastVolumeMl = weight * dose`
- `slotMinutes = round(60 / capacity)`
- `prepFactor = 1.15` for `Cardiac CT`
- `prepFactor = 1.10` for `Chest MRI`
- `prepFactor = 1.00` for `General XRay`
- `prepMinutes = round(slotMinutes * prepFactor)`

### Input constraints to preserve initially
- Weight: `10.0` to `300.0`
- Dose: `0.1` to `5.0`
- Capacity: `1` to `12`

### Proposed route
- `/workflows/protocol-planning`

### Page goal
Turn a simple desktop calculation form into a modern decision-support workspace.

### Web-first page layout
Use a three-zone layout:
1. **Page header**
   - title
   - short subtitle
   - quick actions
2. **Planning input panel**
   - exam type selector
   - patient and scanner inputs
   - calculate and reset actions
3. **Recommendation workspace**
   - key metrics cards
   - operational guidance summary
   - optional schedule impact panel
   - optional trends or comparison area later

### Recommended page composition
- `PageHeader`
- `ContextHeader`
- `FormSection`
- `FormField`
- `StatCard`
- `Alert`
- `StatusBadge`
- `Drawer`
- `EmptyState`
- `Toast`

### Proposed user flow
1. User opens the Protocol Planning Workbench.
2. User sees sensible defaults already populated.
3. User adjusts exam type, patient weight, dose, and scanner capacity.
4. The interface recalculates on demand or in real time depending on the final UX decision.
5. The user receives clear metric cards for contrast volume, slot duration, and prep buffer.
6. The user sees an operational interpretation in plain language.
7. The user can reset the scenario or save/share it later in a future phase.

### UX redesign direction
The web version should not render a plain form over a plain result box.

Instead, it should present:
- a premium planning canvas
- strong visual hierarchy
- summary metrics above detailed explanation
- guided input layout
- contextual validation
- concise healthcare-appropriate language

### Suggested web UI sections

#### Section 1: Header
- Title: `Protocol Planning Workbench`
- Subtitle: `Plan contrast volume, slot duration, and prep timing for imaging workflows.`
- Actions:
  - `Reset`
  - future `Save Scenario`
  - future `Export Summary`

#### Section 2: Scenario inputs
Fields:
- Exam type
- Patient weight
- Target dose
- Scanner capacity

Recommended presentation:
- one card or panel
- short helper text under each field
- strong inline validation
- immediate indication when values are clamped or corrected

#### Section 3: Outcome summary
Use top-level metric cards for:
- `Calculated Contrast Volume`
- `Estimated Slot Duration`
- `Suggested Prep Buffer`

#### Section 4: Operational guidance
Show a narrative summary such as:
- recommended planning interpretation
- staffing or scheduling note
- caution if values are near operational limits

#### Section 5: Future expansion area
Reserve space for later enhancements such as:
- compare two scenarios
- staffing impact estimate
- schedule density visualization
- protocol recommendation history

### Data model for the first web version

#### Input model
```text
ProtocolPlanningScenario
- examType: 'General XRay' | 'Cardiac CT' | 'Chest MRI'
- patientWeightKg: number
- targetDoseMlPerKg: number
- scannerCapacityPerHour: number
```

#### Result model
```text
ProtocolPlanningResult
- contrastVolumeMl: number
- estimatedSlotMinutes: number
- suggestedPrepMinutes: number
- operationalNote: string
- status: 'draft' | 'calculated'
```

### Form behavior recommendation
For the first version:
- use controlled form submission with a visible `Calculate` action
- validate on blur and on submit
- allow a future enhancement to support live recalculation

Reason:
- it matches the current mental model
- it is safer for a first workflow
- it keeps the interaction clear for demo audiences

### API strategy for the pilot
The pilot can start with a mock or local calculation layer.

#### Phase 1 option
Perform calculation entirely in the frontend using the same logic as the current desktop app.

#### Phase 2 option
Move calculation behind an API endpoint if the business logic grows or needs auditing.

### Suggested feature structure

```text
features/protocol-planning/
  api/
    calculate-protocol-plan.ts
  components/
    protocol-planning-form.tsx
    protocol-planning-summary.tsx
    protocol-planning-metrics.tsx
    protocol-planning-guidance.tsx
  hooks/
    use-protocol-planning-form.ts
  schemas/
    protocol-planning.schema.ts
  types/
    protocol-planning.types.ts
  utils/
    calculate-protocol-plan.ts
    protocol-planning-mappers.ts
```

### Recommended validation rules
- Weight must be numeric and within allowed range.
- Dose must be numeric and within allowed range.
- Capacity must be numeric, whole-number compatible, and within allowed range.
- Exam type must be one of the supported values.

### Suggested demo-quality details
To create a strong first impression:
- animate metric card transitions subtly
- show clean loading or recalculation feedback
- highlight the operational note as an executive summary
- use premium typography and spacing
- avoid cluttered dense data entry layouts

### Accessibility notes for this workflow
- full keyboard support for all fields and actions
- readable helper text and validation copy
- live region or equivalent announcement for recalculated results if needed
- visible focus states on all controls

### First pilot acceptance criteria
- The page loads with current default values.
- A user can calculate a scenario using the current logic.
- The results match the existing MFC/WPF formulas.
- Reset returns the page to default values.
- Validation prevents invalid values from producing unclear output.
- The page demonstrates the intended premium visual direction.
- The implementation establishes reusable patterns for forms, summary cards, and status messaging.

### What this pilot should prove
This first workflow should prove that the web rewrite can:
- preserve business intent
- dramatically improve visual quality
- improve usability without preserving desktop layout
- establish reusable architecture and component patterns

## First Milestone Blueprint

The first implementation milestone should deliver a thin but polished vertical slice.

### Milestone 1 scope
- Next.js app scaffold
- application shell
- design token foundation
- sidebar and top bar
- dashboard page shell
- one pilot workflow page
- mock API layer or placeholder services
- one shared table pattern
- one shared form pattern
- loading, empty, and error states

### Milestone 1 implementation status (current)
- ✅ `web-app/` scaffold is in place with Next.js + TypeScript + Tailwind baseline files.
- ✅ Pilot route `/workflows/protocol-planning` is implemented.
- ✅ Interactive protocol planning flow is implemented with calculate/reset behavior.
- ✅ Validation and parsing helpers are implemented for protocol inputs.
- ✅ Existing MFC/WPF planning formulas are preserved in the current calculation utility.
- ✅ `web-app` is visible in Solution Explorer via solution-folder entry.
- ✅ Tailwind PostCSS config was updated to use `@tailwindcss/postcss` for current package compatibility.
- ⏳ Shared shell components and provider integrations are still pending.
- ⏳ Automated test coverage for the web feature is still pending.

### Milestone 1 success criteria
- the app feels visually modern immediately
- the shell is reusable for future modules
- at least one workflow demonstrates the target UX direction
- documentation remains aligned with implementation choices

## Environment and Configuration Blueprint

### Environment variable rules
Keep environment access centralized through `lib/config/env.ts`.

Examples of likely variables:
- `NEXT_PUBLIC_API_BASE_URL`
- `NEXT_PUBLIC_APP_NAME`
- `NEXT_PUBLIC_ENABLE_MOCKS`

### Configuration rules
- Do not read environment variables directly across the UI.
- Parse and validate environment values in one place.
- Expose a typed configuration object to the rest of the app.

## Testing Blueprint

### Recommended test layers
- **Unit tests** for utility functions, mapping logic, and schemas
- **Component tests** for reusable UI building blocks
- **Integration tests** for key workflows and forms
- **End-to-end tests** for demo-critical scenarios

### Suggested testing tools
- **Vitest** for unit and component tests
- **Testing Library** for React component behavior
- **Playwright** for end-to-end coverage

### Priority test targets
Test these first:
- navigation shell behavior
- shared table behavior
- form validation behavior
- document or workflow save path
- route-level loading and empty states

## Coding Standards for the Web App

### General rules
- Use strict TypeScript settings.
- Prefer explicit types at public boundaries.
- Keep functions small and intention-revealing.
- Avoid overly clever abstractions early.
- Build reusable primitives only after patterns are confirmed.

### React rules
- Keep page files thin.
- Move reusable logic into hooks or feature utilities.
- Keep feature ownership clear.
- Avoid prop-drilling across too many layers when a small local context or store is more appropriate.

### Styling rules
- Use Tailwind utilities for most layout and styling.
- Extract repeated patterns into shared components instead of large utility-class duplication.
- Keep one source of truth for tokens.
- Avoid ad hoc one-off styling when a design-system token already exists.

### Performance rules
- Stream and render server content where practical.
- Code-split naturally by route and feature.
- Use virtualization later if heavy tables require it.
- Optimize perceived speed with skeletons and progressive rendering.

---

## Reproducibility Rules for This Project

All important architectural and product decisions should be captured in this file or linked decision records.

### Record the following when they occur
- framework choices
- library additions or removals
- routing strategy changes
- design token changes
- accessibility standards
- workflow redesign decisions
- API integration assumptions
- demo-specific visual requirements
- tradeoffs accepted during delivery

### Suggested decision record format
Use entries like:

```text
Date:
Decision:
Status:
Reason:
Impact:
Next action:
```

---

## Decision Log

### Decision 001
- **Decision**: Build the new rewrite as a web application instead of another desktop rewrite.
- **Status**: Accepted
- **Reason**: The goal is to modernize the experience and present a fresh, current product direction.
- **Impact**: The UI can be redesigned around web-native workflows instead of desktop parity.

### Decision 002
- **Decision**: Do not visually match the MFC or WPF applications.
- **Status**: Accepted
- **Reason**: The target outcome is a significantly better, modern, premium experience.
- **Impact**: Redesign is encouraged as long as core business intent is preserved.

### Decision 003
- **Decision**: Use Next.js + React + TypeScript as the frontend foundation.
- **Status**: Accepted
- **Reason**: Best mix of maintainability, UI quality, ecosystem strength, and delivery speed.
- **Impact**: Future implementation planning should assume a React/Next.js architecture.

### Decision 004
- **Decision**: Use Tailwind CSS + shadcn/ui + Framer Motion for the visual system.
- **Status**: Accepted
- **Reason**: This is the fastest path to a polished, modern, premium interface.
- **Impact**: Shared components and design tokens should align to this stack.

### Decision 005
- **Decision**: Organize the web app by features and workflows, not by desktop views.
- **Status**: Accepted
- **Reason**: Web applications work best when navigation maps to user tasks.
- **Impact**: Discovery should focus on workflows rather than one-for-one screen cloning.

### Decision 006
- **Decision**: Use the Next.js App Router with server components by default.
- **Status**: Accepted
- **Reason**: This provides a modern, scalable baseline with good performance and clear route composition.
- **Impact**: Client components should be introduced selectively for interactivity only.

### Decision 007
- **Decision**: Use feature-based folder organization inside the web app.
- **Status**: Accepted
- **Reason**: This supports domain ownership, scalability, and cleaner evolution of workflows.
- **Impact**: Shared code should remain intentional and not become a dumping ground.

### Decision 008
- **Decision**: Use TanStack Query for server state, Zustand for lightweight UI state, and React Hook Form with Zod for forms.
- **Status**: Accepted
- **Reason**: This keeps state responsibilities clear and avoids over-centralized client state.
- **Impact**: Future implementation should follow these boundaries rather than introducing a monolithic store.

### Decision 009
- **Decision**: Build a reusable application shell and design token layer before scaling feature delivery.
- **Status**: Accepted
- **Reason**: The shell and token foundation will define the premium feel and reduce later inconsistency.
- **Impact**: Early implementation should prioritize shared UX primitives over isolated page work.

### Decision 010
- **Decision**: Use the Protocol Planning Workbench as the first pilot workflow for the web rewrite.
- **Status**: Accepted
- **Reason**: It provides the strongest combination of visual transformation, manageable scope, clear business story, and reusable form-driven architecture.
- **Impact**: The first implementation milestone should prioritize the `/workflows/protocol-planning` route and its supporting components, schemas, and calculation logic.

### Decision 011
- **Decision**: Keep `web-app` in the existing repository and expose it in `MFCSDIAppln.sln` as a solution folder (not a native build project).
- **Status**: Accepted
- **Reason**: This keeps the web rewrite discoverable in Solution Explorer while avoiding incorrect treatment as a `.csproj`/`.vcxproj` project type.
- **Impact**: Day-to-day web development should run from the `web-app` folder using npm scripts and a JS/TS-focused editor workflow.

---

## Open Questions

The following should be answered before implementation begins:
- What are the real business workflows behind the current demo screens?
- Which workflow matters most for customer demos?
- Does the web version need role-based experiences?
- What backend or service layer will the web UI call?
- Will the web app need authentication in the first milestone?
- Is responsive tablet support required from day one?
- Which parts of the current desktop app are only demo placeholders versus real workflow requirements?
- Does document editing require plain text, rich text, or structured clinical content?

---

## Future Findings and Decisions

Add future discoveries below using this format.

### Template Entry
- **Date**:
- **Category**: Finding | Decision | Risk | Assumption | Open Question
- **Title**:
- **Details**:
- **Impact**:
- **Owner**:
- **Next Step**:

### Entries
- **Date**: 2026-06-21
- **Category**: Decision
- **Title**: Concrete frontend blueprint added
- **Details**: The strategy now includes a concrete Next.js project structure, route map, provider model, token guidance, state rules, and first milestone blueprint.
- **Impact**: Future implementation can start from a documented architectural baseline instead of revisiting stack and structure decisions.
- **Owner**: Copilot + project owner
- **Next Step**: Confirm the first pilot workflow and backend/API assumptions.

- **Date**: 2026-06-21
- **Category**: Decision
- **Title**: Pilot workflow selected as Protocol Planning Workbench
- **Details**: The first web workflow will be the protocol planning experience derived from the current MFC and WPF form views, preserving the existing calculation model while redesigning the UX as a modern decision-support workspace.
- **Impact**: The first web implementation should center on route `/workflows/protocol-planning`, with reusable patterns for forms, summary metrics, guidance panels, and validation.
- **Owner**: Copilot + project owner
- **Next Step**: Define the implementation scaffold for the route, feature module, schema, and calculation utilities.

- **Date**: 2026-06-21
- **Category**: Finding
- **Title**: Pilot workflow implementation completed (initial interactive slice)
- **Details**: The web app scaffold and interactive Protocol Planning Workbench page were implemented with calculate/reset actions, validation/parsing helpers, and preserved planning formulas. The web folder is now visible in the solution as a solution folder.
- **Impact**: The project now has a working end-to-end pilot slice and can proceed to shell components, provider integration, and test coverage.
- **Owner**: Copilot + project owner
- **Next Step**: Refactor the pilot page into reusable components and add automated tests for calculation/validation utilities.

---

## Recommended Next Actions

1. Refactor the protocol-planning page into reusable UI components and reduce inline styling.
2. Build the shared app shell (sidebar, top bar, page header) and apply it to the pilot route.
3. Add provider integrations (query client and future app-level providers).
4. Add unit tests for calculation and validation/parsing utilities.
5. Define the second workflow slice (structured report editor) and its route/module plan.
6. Prepare demo script notes comparing MFC/WPF vs web pilot improvements.

---

## Short Final Recommendation

If the project starts now, the recommended direction is:
- **Framework**: Next.js
- **Language**: TypeScript
- **Styling**: Tailwind CSS
- **Component approach**: shadcn/ui-based design system
- **Motion**: Framer Motion
- **Server state**: TanStack Query
- **Client UI state**: Zustand
- **Forms and validation**: React Hook Form + Zod

This gives the best chance of delivering a healthcare web app that feels current, premium, and genuinely impressive rather than like a web copy of an old desktop application.
