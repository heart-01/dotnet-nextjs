import type { Metadata } from "next";
import { AppRouterCacheProvider } from "@mui/material-nextjs/v13-appRouter";
import { ThemeProvider } from "@mui/material/styles";
import Themes from "./theme/Themes";

import "./globals.css";

import Header from "@/app/components/front/header/Header";
import Footer from "@/app/components/front/footer/Footer";

export const metadata: Metadata = {
  title: {
    template: "%s | Next Store",
    default: "Home | Next Store",
  },
  description: "Next Store - E-commerce website built with Next.js and Material-UI",
  keywords: ["Next.js", "Material-UI", "E-commerce", "Next Store"],
  authors: [{ name: "StoreAPP Team" }],
  icons: "@/favicon.ico",
  robots: "index, follow",
};

export default function RootLayout({
  children,
}: Readonly<{
  children: React.ReactNode;
}>) {
  return (
    <html lang="en">
      <body>
        <AppRouterCacheProvider>
          <ThemeProvider theme={Themes}>
            <Header />
            {children}
            <Footer />
          </ThemeProvider>
        </AppRouterCacheProvider>
      </body>
    </html>
  );
}
