# 6 DoF Robotic Motion Phantom

Robotic Motion Phantom is an open source software for performing quality assurance tests of real-time image-guided radiotherapy systems. The Robotic Motion Phantom application provides methods to implement the appropriate sequence of transformations to accurately reproduce the full range (6 DoF translational and rotational motion in the IEC coordinate system) and rate of patient-measured tumour motion using a robotically controlled phantom to provide precise geometric quality assurance for advanced radiotherapy approaches. The set-up and motion trace repeatability of the robotic motion system was evaluated against the Calypso system and was shown to have sub-mm and sub-degree accuracy and precision in translational and rotational degrees of freedom respectively. For more details, please see [this publication](https://doi.org/10.1088/1361-6560/ab1935).

## Key Features

- Accurate motion reproducibility for 6DoF tumour motion.
- A user-friendly GUI interface.
- Design compactness.
- Easy workflow.


## Getting Started
### Requirements
    
  * Hardware
     - A six-axis robot (Tested on a commercially available [UR3 Universal Robot](https://www.universal-robots.com/products/ur3-robot/)) and its associated hardware
     - A phantom
  
  * Software 
     - Microsoft Visual Studio 2019 with .NET desktop development workload
     - Winforms
  
  * Third-party libraries (located in `External Libraries/`)
     - EasyModbusTCP v4.2
     - LiveCharts v0.9.7
     - LiveCharts.WinForms v0.9.7.1
     - LiveCharts.Wpf v0.9.7
     - Math.Net Numerics v4.12.0
     


### Building the Robotic Motion Phantom

The software was written and tested in C# using Visual Studio 2019 with .NET Framework 4.0 using Windows 10, but may work with other versions of Visual Studio.

1. Open `PhantomControl.sln` in Visual Studio.
2. Add all the five external libraries listed above using the References tab in the Solution Explorer. 
3. Build the solution in Debug mode (or in Release mode).
4. This will create the application `PhantomControl\bin\Debug(or Release)\PhantomControl.exe` which can then be operated using [this guide](https://github.com/ACRF-Image-X-Institute/6-DoF-Robotic-Motion-Phantom/blob/main/Documentation/Software%20GUI%20Guide.pdf) when connected to a six-axis robotic system.

## Documentation

Please see the [documentation](https://github.com/ACRF-Image-X-Institute/6-DoF-Robotic-Motion-Phantom/tree/main/Documentation) for  instructions on operating the robotic phantom and relevant information related to the latest release of this software. 

## Publications

- S. Alnaghy et al., Phys. Med. Biol. 64 105021 (2019), [doi:10.1088/1361-6560/ab1935](https://doi.org/10.1088/1361-6560/ab1935)
- Shi K. et al., Med Phys. 47(12):6068-6076 (2020), [doi: 10.1002/mp.14502](https://aapm.onlinelibrary.wiley.com/doi/full/10.1002/mp.14502)

## Clinical Trials

- Liver Ablative Radiotherapy utilizing KIM, [TROG 17.03 LARK trial](https://www.trog.com.au/1703-LARK) 
- Novel Integration of New prostate radiation therapy schedules with adJuvant Androgen deprivation, [TROG 18.01 NINJA trial](https://www.trog.com.au/1801-NINJA)

## Licence

Content is released under [this licence](https://github.com/ACRF-Image-X-Institute/6-DoF-Robotic-Motion-Phantom/blob/a5fb87378eb501c1a9539277ff3f0080b794489e/Copyright%20Notice%20and%20Licence.pdf) which includes attribution guidelines, contribution terms, and software and third-party licences and permissions.

## Authors

Dr. Saree Alnaghy, Dr. Chandrima Sengupta, Kuldeep Makhija.

## Acknowledgements

The authors thank all the contributors, Dr. Andre Kyme, Dr. Vincent Caillet, Dr. Doan Trang Nguyen, Dr. Ricky O'Brien, Dr. Jeremy T. Booth and Prof. Paul J Keall for lending their valuable input and expertise leading up to the initial release. Thanks to Dr. Melissa Mail and Dr. Sara Tomka for their help in producing the licence of this software and to the Image-X team for providing valuable input in developing this repository.
