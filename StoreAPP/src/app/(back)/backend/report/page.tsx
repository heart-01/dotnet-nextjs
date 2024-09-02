import React from "react";
import ReportPage from "./Report";
import { Metadata } from "next";

type Props = {};

export const metadata: Metadata = {
  title: "Report",
  description: "Report page description",
  keywords: ["Report", "Next.js"],
};

const Report = (props: Props) => {
  return <ReportPage />;
};

export default Report;
