import Footer from "@/components/front/Footer";
import Navbar from "@/components/front/Navbar";

const FrontLayout = ({
  children,
}: Readonly<{
  children: React.ReactNode;
}>) => {
  return (
    <>
      <Navbar />
      <div>{children}</div>
      <Footer />
    </>
  );
};

export default FrontLayout;
