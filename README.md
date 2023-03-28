# Totally Money Test

### Requirements
.NET 6.0

### How to run
run
> dotnet run [file]

where [file] is the path to a valid JSON input file.


### Input file format
This program takes a JSON file as input, an example file
named "ExampleInput.json" has been provided.

Schema: 
```
[
    {
        "name": String,
        "frequency": { 
            "type": String,
            update: Int | Int[] | null
        }
    }
]
```