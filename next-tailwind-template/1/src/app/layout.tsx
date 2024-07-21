import type { Metadata } from "next";
import { Inter } from "next/font/google";
import "./globals.css";
import PrelineScript from "@/components/PrelineScript";

const inter = Inter({ subsets: ["latin"] });

export const metadata: Metadata = {
  title: {
    template: "%s | Next Template",
    default: "Next Template | Next Template",
  },
  description: "NextJs Tailwind CSS Multipurpose Landing Page Template",
  keywords: ["Next.js", "Tailwind CSS", "Multipurpose", "Landing Page"],
  authors: [{ name: "Team", url: "https://next-template.co.th" }],
  icons: "/favicon.ico",
  robots: "index, follow",
};

export default function RootLayout({
  children,
}: Readonly<{
  children: React.ReactNode;
}>) {
  return (
    <html lang="en">
      <body className={inter.className}>{children}</body>
      <PrelineScript />
    </html>
  );
}
