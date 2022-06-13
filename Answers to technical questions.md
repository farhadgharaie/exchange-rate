# Technical questions

``
1- How long did you spend on the coding assignment? What would you add to your solution if you had more time? If you didn't spend much time on the coding assignment then use this as an opportunity to explain what you would add.
``
 
 It took 6 days, each day 5 or 6 hours to commit the assignment. I try to use many principles to refactor the code. 
 I think the result could cache in the memory for a configurable time and response to requests for in memory crypto symbol provided much faster than call third party APIs.

``
 2- What was the most useful feature that was added to the latest version of your language of choice? Please include a snippet of code that shows how you've used it.
``
 
  I am using .Net core 3.1 and C# 8 for the assessment but the latest version for .Net Core is 6 and for C# is 11 that the most useful features are:
   - Generic attributes
   - List patterns
 
  Also, the C# 8 has features like :
   - Property patterns, Switch expressions.
   - Using declarations
 
``
3- How would you track down a performance issue in production? Have you ever had to do this?
``

 By monitoring the time between requests and responses, errors or exceptions - send to email or gathered by data processing pipeline-that occured in a specific time or alarmed by a monitoring system can find the performance issues.Moreover, There are may several external source like databases, 3rd party services or network perfomance that should monitored by agents to collect data and process for issues.
 I used ELK stack (Elasticsearch, Logstash, Kibana) to gather all events in a production and have a monitoring for different responses. Also, Used Seqlog for all of productions, generally. 

``
4- What was the latest technical book you have read or tech conference you have been to? What did you learn?
``
- About 1 year ago studied `Clean Architecture` by `Robert C.Martin`. 
  Similar to the onion and hexagonal architectures, clean architecture has a separation of concern objective which divides the software into different layers. The Framework Independence, Testable, UI, and external agency independence are the main key to the architecture.
  There are four-layer, Entities, Use Cases, Interface Adapters, and Frameworks-and-Drivers, but there is no rule that says you must always have just these four. However, The  Dependency Rule, which points that source code dependencies can only point inwards, is always applied. 

- 2 Months ago, `Building Microservices (Designing fine-grained systems)` by `Sam Newman`. 
 Microservices are small, autonomous services that work together.  They are small with the definition of cohesion which drives to have related code grouped. It is the same as the Single Responsibility Principle, which states "Gather together those things that change for the same reason, and separate those things that change for different reasons".
 They are autonomous which state, that services need to be able to change independently of each other and be deployed by themselves without requiring consumers to change. 
 It also pointed out that great software comes from great people.

``
5- What do you think about this technical assessment?
``

 This is a multifaceted assessment of the capabilities, experiences, and uses of third-party APIs. Applicant evaluation will be accomplished by clean, secure, quality code items using unit tests and object-oriented programming.
 Also, questions about the latest technical books read and tracking performance issues in the product will help evaluate the motivation and knowledge of a software engineer.
 In general, this technical assessment was different from my previous assessments.


``
6- Please, describe yourself using JSON.
``
```json
{
   "identification":{
      "firstName":"Farhad",
      "lastName":"Gharaie",
      "Address":{
        "city": "Tehran",
        "country": "Iran"
      },
      "email":"f.gharaie@gmail.com",
      "phone":"+98-912-245-6354"
   },
   "skills":[
     {
       "soft" :
       [
         {
          "category":"behaviour",
          "describe":"Energetic,Friendly,Honest,Optemistic,Warmhearted"
         },
         {
          "category":"team",
          "describe":"Disciplined,Independent,Hard-working"
         }
        ]
    },
     {
       "hard":
     [
       {
          "category":"language",
          "describe":"C#, .Net, NodeJS"
         },
         {
          "category":"database",
          "describe":"SQL, MongoDB"
         },
         {
          "category":"test framework",
          "describe":"xUnit, fluentAssertion, Specflow, selenium"
         },
         {
          "category":"task management",
          "describe":"TFS"
         },
         {
          "category":"repository",
          "describe":"git"
         }
     ]
     }
     ],
   "experiences":[
      {
         "title":"Software engineer",
         "companyName":"Informatics Services Corporation",
         "location":"Tehran, Iran",
         "startDate":{
            "month":"October",
            "year":"2009"
         },
         "endDate":{
            "month":"April",
            "year":"2022"
         }
      }
   ],
   "trainings":[
      {
         "title":"DDD workshop",
         "year":"2022"
      },
      {
         "title":"Event Sourcing and CQRS",
         "year":"2021"
      },
      {
         "title":"Scrum Professional and XP Programming",
         "year":"2017"
      }
   ],
   "educations":[
      {
         "degree":"Master",
         "field":"Information technology management",
         "university":"Tehran University"
      },
      {
         "degree":"Bachelor",
         "field":"Software engineer",
         "university":"Shamsipor technical university"
      }
   ],
   "activities":[
   {
     "sport":"Hiking,Jogging,Swimming"
   },
   {
     "extra":"Founder of a charity since 2005"
   }]
}
```

**✨Thanks for your time✨**

`Farhad Gharaie` 😃