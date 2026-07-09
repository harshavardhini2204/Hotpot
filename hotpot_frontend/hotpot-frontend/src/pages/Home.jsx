import Hero from "../components/Hero";
import CategorySection from "../components/CategorySection";
import FeaturedRestaurant from "../components/FeaturedRestaurant";
import WhyChooseUs from "../components/WhyChooseUs";
import Footer from "../components/Footer";

function Home() {
    return (
        <>
            <Hero />
            <CategorySection />
            <FeaturedRestaurant />
            <WhyChooseUs />
            <Footer />
        </>
    );
}

export default Home;