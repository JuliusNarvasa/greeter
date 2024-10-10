import React, { FC, useEffect, useState } from 'react';
import { TypeAnimation } from 'react-type-animation';

const Home: FC = () => {
  const [name] = useState<string>("Bambaw");
  const [baseUrl, setBaseUrl] = useState<string | undefined>(undefined);
  const [greeting, setGreeting] = useState<{languageName: string, countryOfOrigin: string, morning: string, afternoon: string, evening: string} | undefined>(undefined)
  const [showGreeting, setShowGreeting] = useState<boolean>(false)

  useEffect(() => {
    if (process.env.NODE_ENV === 'development') {
      setBaseUrl('http://localhost:5079/')
    } else if (process.env.NODE_ENV === 'production') {
      setBaseUrl('http://localhost:5079/')
    }
  }, [])

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
        setShowGreeting(true)
      })
      .catch((err) => {
        console.log(err)
      })
    }
  }, [name, baseUrl])

  return (
    <>
      <div id="stars"></div>
      <div id="stars2"></div>
      <div id="stars3"></div>
      <div className="container">
        <div id="title">
          <span>
            {greeting && <TypeAnimation
              sequence={[
                greeting.morning,
              ]}
              wrapper='span'
              speed={25}
              style={{ fontSize: '1em', display: 'inline-block' }}
              cursor={false}
            />}
            <span className='blinking-cursor'> _</span>
          </span>
        </div>
      </div>
    </>
    
  );
};

export default Home;

