import React, { FC, useEffect, useState } from 'react';
import { TypeAnimation } from 'react-type-animation';

const Home: FC = () => {
  const [name] = useState<string>("Bambaw");
  const [baseUrl, setBaseUrl] = useState<string | undefined>(undefined);
  const [greeting, setGreeting] = useState<{languageName: string, countryOfOrigin: string, morning: string, afternoon: string, evening: string} | undefined>(undefined)
  const [timeOfDay, setTimeOfDay] = useState<"Morning" | "Afternoon" | "Evening">("Morning")
  const [showGreeting, setShowGreeting] = useState<boolean>(false)
  const [greetingText, setGreetingText] = useState<string | undefined>(undefined);

  useEffect(() => {
    if (process.env.NODE_ENV === 'development') {
      setBaseUrl('http://localhost:5079/')
    } else if (process.env.NODE_ENV === 'production') {
      setBaseUrl('http://localhost:5079/')
    }

    getTimeOfDay();
  }, [])

  useEffect(() => {
    if (greeting !== undefined) {
      if (timeOfDay === "Morning") {
        setGreetingText(greeting?.morning)
      } else if (timeOfDay === "Afternoon") {
        setGreetingText(greeting?.afternoon)
      } else if (timeOfDay === "Evening") {
        setGreetingText(greeting?.evening)
      }
      setShowGreeting(true)
    }
    
  }, [timeOfDay, greeting])

  useEffect(() => {
    if (name !== undefined && baseUrl !== undefined) {
      const formData = new FormData();
      formData.append('name', name);
      fetch(baseUrl + 'Greeter/get-greeting', {
        method: 'POST',
        body: formData
      })
      .then(res => res.json())
      .then((data) => {
        document.title = data.languageName
        setGreeting(data)
      })
      .catch((err) => {
        console.log(err)
      })
    }
  }, [name, baseUrl])

  function getTimeOfDay() {
    const now = new Date();
    const hours = now.getHours();
  
    if (hours >= 0 && hours < 12) {
      setTimeOfDay('Morning');
    } else if (hours >= 12 && hours < 18) {
      setTimeOfDay('Afternoon');
    } else {
      setTimeOfDay('Evening');
    }
  }

  return (
    <>
      <div id="stars"></div>
      <div id="stars2"></div>
      <div id="stars3"></div>
      <div className="container">
        <div id="title">
          <div id="animation-container">
            {showGreeting && greetingText !== undefined && <TypeAnimation
              sequence={[
                greetingText,
              ]}
              wrapper='span'
              speed={25}
              style={{ display: 'inline-block' }}
              cursor={false}
              className='greeting-text'
            />}
            <span className='blinking-cursor greeting-text'> _</span>
          </div>
        </div>
      </div>
    </>
    
  );
};

export default Home;

