import "./Hero.css";

function Hero() {
    return (
        <section className="hero">

            <div className="hero-overlay"></div>

            <div className="container hero-content">

                <h1>
                    Hungry?
                </h1>

                <h2>
                    Order Your Favourite Food Anytime
                </h2>

                <p>
                    Discover top restaurants, delicious dishes,
                    and lightning-fast delivery near you.
                </p>

                <div className="hero-search">

                    <input
                        type="text"
                        className="form-control form-control-lg"
                        placeholder="Search restaurants, burgers, pizza..."
                    />

                    <button className="btn btn-warning btn-lg">
                        Search
                    </button>

                </div>

            </div>

        </section>
    );
}

export default Hero;