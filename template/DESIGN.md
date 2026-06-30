---
name: Lumina Tech
colors:
  surface: '#f7f9fb'
  surface-dim: '#d8dadc'
  surface-bright: '#f7f9fb'
  surface-container-lowest: '#ffffff'
  surface-container-low: '#f2f4f6'
  surface-container: '#eceef0'
  surface-container-high: '#e6e8ea'
  surface-container-highest: '#e0e3e5'
  on-surface: '#191c1e'
  on-surface-variant: '#424754'
  inverse-surface: '#2d3133'
  inverse-on-surface: '#eff1f3'
  outline: '#727785'
  outline-variant: '#c2c6d6'
  surface-tint: '#005ac2'
  primary: '#0058be'
  on-primary: '#ffffff'
  primary-container: '#2170e4'
  on-primary-container: '#fefcff'
  inverse-primary: '#adc6ff'
  secondary: '#565e74'
  on-secondary: '#ffffff'
  secondary-container: '#dae2fd'
  on-secondary-container: '#5c647a'
  tertiary: '#924700'
  on-tertiary: '#ffffff'
  tertiary-container: '#b75b00'
  on-tertiary-container: '#fffbff'
  error: '#ba1a1a'
  on-error: '#ffffff'
  error-container: '#ffdad6'
  on-error-container: '#93000a'
  primary-fixed: '#d8e2ff'
  primary-fixed-dim: '#adc6ff'
  on-primary-fixed: '#001a42'
  on-primary-fixed-variant: '#004395'
  secondary-fixed: '#dae2fd'
  secondary-fixed-dim: '#bec6e0'
  on-secondary-fixed: '#131b2e'
  on-secondary-fixed-variant: '#3f465c'
  tertiary-fixed: '#ffdcc6'
  tertiary-fixed-dim: '#ffb786'
  on-tertiary-fixed: '#311400'
  on-tertiary-fixed-variant: '#723600'
  background: '#f7f9fb'
  on-background: '#191c1e'
  surface-variant: '#e0e3e5'
typography:
  display:
    fontFamily: Inter
    fontSize: 64px
    fontWeight: '700'
    lineHeight: 72px
    letterSpacing: -0.02em
  headline-lg:
    fontFamily: Inter
    fontSize: 40px
    fontWeight: '600'
    lineHeight: 48px
    letterSpacing: -0.01em
  headline-lg-mobile:
    fontFamily: Inter
    fontSize: 32px
    fontWeight: '600'
    lineHeight: 40px
  headline-md:
    fontFamily: Inter
    fontSize: 24px
    fontWeight: '600'
    lineHeight: 32px
  body-lg:
    fontFamily: Inter
    fontSize: 18px
    fontWeight: '400'
    lineHeight: 28px
  body-md:
    fontFamily: Inter
    fontSize: 16px
    fontWeight: '400'
    lineHeight: 24px
  label-md:
    fontFamily: Inter
    fontSize: 14px
    fontWeight: '500'
    lineHeight: 20px
    letterSpacing: 0.01em
  label-sm:
    fontFamily: Inter
    fontSize: 12px
    fontWeight: '600'
    lineHeight: 16px
rounded:
  sm: 0.25rem
  DEFAULT: 0.5rem
  md: 0.75rem
  lg: 1rem
  xl: 1.5rem
  full: 9999px
spacing:
  base: 4px
  xs: 0.5rem
  sm: 1rem
  md: 1.5rem
  lg: 2.5rem
  xl: 4rem
  gutter: 24px
  margin-mobile: 16px
  margin-desktop: 48px
---

## Brand & Style

The brand personality is high-end, precise, and forward-thinking. It targets a discerning tech audience that values both performance and aesthetics. The UI should evoke a sense of reliability and cutting-edge innovation through a **Modern Minimalist** style.

The design system focuses on high-quality product presentation using expansive whitespace, refined typography, and subtle depth. It avoids visual clutter to ensure that the hardware photography remains the focal point. The emotional response should be one of "effortless sophistication"—where the interface feels as well-engineered as the premium electronics it showcases.

## Colors

The palette is anchored by a high-contrast foundation of Slate and White to establish a professional, "pro-grade" atmosphere. 

- **Primary:** Electric Blue (#3b82f6) is used exclusively for primary actions, progress indicators, and active states to drive conversion.
- **Surface:** In Light Mode, use a mix of pure white (#ffffff) for primary cards and a very light Slate-50 (#f8fafc) for background sections to create subtle hierarchy.
- **Dark Mode:** Transition to a deep Slate-950 (#020617) background. Use Slate-900 for elevated surfaces (cards, modals) to maintain depth.
- **Accents:** Use success (emerald), warning (amber), and error (rose) sparingly, keeping them desaturated to match the premium tech aesthetic.

## Typography

This design system utilizes **Inter** for its systematic, utilitarian, and modern character. The typographic hierarchy is designed to guide the user through technical specifications and marketing copy with ease.

- **Headlines:** Use tighter letter-spacing on larger sizes to create a "locked-in" professional look. 
- **Body Text:** Standard weight (400) is used for readability, while Medium (500) is reserved for sub-headers or emphasized specs.
- **Product Specs:** Use the `label-sm` style with uppercase transformations for technical categories (e.g., "PROCESSOR", "BATTERY LIFE") to create a structured, data-driven feel.

## Layout & Spacing

The layout follows a **Fluid Grid** model with a max-width of 1440px for desktop to prevent line lengths from becoming unreadable.

- **Grid:** 12-column grid on desktop, 8-column on tablet, and 4-column on mobile.
- **Rhythm:** Use an 8px base unit for all spacing components. 
- **Product Grids:** Maintain generous gutters (24px+) to allow product images breathing room, preventing the UI from feeling "discount" or cluttered.
- **Vertical Spacing:** Use `xl` (64px) spacing between major sections (e.g., Hero to Featured Products) to emphasize the premium nature of the brand.

## Elevation & Depth

Visual hierarchy is achieved through a combination of **Tonal Layering** and **Ambient Shadows**.

- **Surface Tiers:** Backgrounds use the base neutral color. Elevated elements (Product Cards, Navigation Bars) use a pure white (or Slate-900 in dark mode) background.
- **Shadows:** Use extremely soft, large-radius shadows with low opacity (4-6%). Shadows should feel like natural ambient occlusion rather than harsh drop shadows. 
- **Borders:** Use thin, 1px soft borders (#e2e8f0 in light, #1e293b in dark) to define card boundaries when shadows are insufficient.
- **Interaction:** On hover, cards should slightly lift (y-axis shift) and the shadow should become more diffused to provide tactile feedback.

## Shapes

The design system uses a **Rounded** language to soften the "industrial" feel of tech products, making them feel approachable.

- **Primary Radius:** 0.5rem (8px) for standard inputs and small buttons.
- **Large Radius (2xl):** 1rem (16px) for product cards, image containers, and main CTA buttons.
- **Extra Large Radius:** 1.5rem (24px) for promotional banners and featured "hero" containers.

## Components

### Buttons
Primary CTAs use the Electric Blue background with white text and a 1rem (16px) corner radius. Secondary buttons should use a "Ghost" style with a 1px border or a subtle Slate-100 background. All buttons have a 200ms transition on hover.

### Product Cards
Cards are the core of the e-commerce experience. They feature a 1:1 aspect ratio for product imagery, a subtle 1px border, and a `rounded-2xl` profile. Content is left-aligned with price emphasized in `headline-md`.

### Input Fields
Inputs use a `rounded-lg` (8px) shape with a light-gray border. On focus, the border transitions to Electric Blue with a subtle 2px outer glow (glow color: `primary-color` at 20% opacity).

### Chips & Badges
Used for product tags (e.g., "New", "Sale", "In Stock"). These should be small, capitalized, and use high-contrast backgrounds with high roundedness (pill-shaped) to distinguish them from actionable buttons.

### Navigation
The sticky top navigation uses a Backdrop Blur effect (Glassmorphism) with 80% opacity to maintain context while scrolling through long product pages.

### Specs Table
Technical specifications should be presented in a clean, alternating-row table with `label-md` for keys and `body-md` for values, ensuring maximum legibility for comparison.