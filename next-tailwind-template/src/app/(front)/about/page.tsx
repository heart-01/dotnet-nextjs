import { Metadata } from "next";
import Content from "./Content";

export const metadata: Metadata = {
  title: "About",
  description: "About page description",
  keywords: ["Contact", "Next.js", "Tailwind CSS"],
};

const About = () => {
  return (
    <>
      <Content />
    </>
  );
};

export default About;
