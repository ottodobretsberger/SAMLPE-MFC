# web-app

This folder contains the initial Next.js-based web rewrite foundation for the healthcare demo application.

## Current status
- manual Next.js scaffold created under `web-app/`
- root app files and route structure added
- design tokens and global styles added
- pilot workflow route added at `/workflows/protocol-planning`
- protocol planning feature types, parsing, and validation helpers added
- interactive protocol planning page implemented (calculate + reset + validation feedback)
- solution entry added so `web-app` is visible in Solution Explorer as a solution folder
- Tailwind PostCSS configuration aligned for current Tailwind package behavior

## Run locally
From `web-app/`:
1. `npm install`
2. `npm run dev` for local development
3. `npm run build` for production build validation

## Next implementation steps
1. extract inline page styling into reusable components and Tailwind classes
2. add shared app shell components (sidebar/top bar/page header)
3. add provider integrations (query client and future app providers)
4. add unit tests for protocol planning calculation and validation
5. start the second workflow candidate (structured report editor)
