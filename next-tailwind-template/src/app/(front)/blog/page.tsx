import { Metadata } from "next";
import Content from "./Content";

export const metadata: Metadata = {
  title: "Blog",
  description: "Blog page description",
  keywords: ["Contact", "Next.js", "Tailwind CSS"],
};

const Blog = () => {
  return (
    <>
      <Content />
    </>
  );
};

export default Blog;
