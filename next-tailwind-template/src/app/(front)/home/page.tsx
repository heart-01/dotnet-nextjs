import { Metadata } from "next";
import Hero from "./Hero";
import Stats from "./Stats";

export const metadata: Metadata = {
  title: "Home",
  description: "Home page description",
  keywords: ["Home", "Next.js", "Tailwind CSS"],
};

const Home = () => {
  return (
    <>
      <Hero />
      <Stats />
    </>
  );
};

export default Home;
