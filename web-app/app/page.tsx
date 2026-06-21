import Link from 'next/link';

export default function HomePage() {
  return (
    <main style={{ padding: '48px' }}>
      <div
        style={{
          maxWidth: '960px',
          margin: '0 auto',
          background: 'var(--color-surface)',
          border: '1px solid var(--color-border)',
          borderRadius: 'var(--radius-xl)',
          boxShadow: 'var(--shadow-md)',
          padding: '40px',
        }}
      >
        <p style={{ color: 'var(--color-primary)', fontWeight: 600, marginTop: 0 }}>Web Rewrite Foundation</p>
        <h1 style={{ marginTop: 0, fontSize: 'var(--font-size-2xl)' }}>Healthcare workflow modernization</h1>
        <p style={{ color: 'var(--color-text-muted)', fontSize: 'var(--font-size-md)', lineHeight: 1.6 }}>
          The first pilot workflow is the Protocol Planning Workbench. This foundation page confirms the Next.js app
          structure is in place and ready for feature implementation.
        </p>
        <div style={{ marginTop: '24px' }}>
          <Link
            href="/workflows/protocol-planning"
            style={{
              display: 'inline-flex',
              alignItems: 'center',
              justifyContent: 'center',
              minHeight: '44px',
              padding: '0 18px',
              borderRadius: '999px',
              background: 'var(--color-primary)',
              color: 'var(--color-primary-foreground)',
              fontWeight: 600,
            }}
          >
            Open Protocol Planning Workbench
          </Link>
        </div>
      </div>
    </main>
  );
}
