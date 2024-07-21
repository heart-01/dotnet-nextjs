import { Metadata } from "next";
import Content from "./Content";

export const metadata: Metadata = {
  title: "Contact",
  description: "Contact page description",
  keywords: ["Contact", "Next.js", "Tailwind CSS"],
};


const Contact = () => {
  return (
    <>
      <Content />
    </>
  );
};

export default Contact;
