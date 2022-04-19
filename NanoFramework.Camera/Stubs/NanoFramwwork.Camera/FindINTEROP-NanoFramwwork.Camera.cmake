#
# Copyright (c) .NET Foundation and Contributors
# See LICENSE file in the project root for full license information.
#

########################################################################################
# make sure that a valid path is set bellow                                            #
# this is an Interop module so this file should be placed in the CMakes module folder  #
# usually CMake\Modules                                                                #
########################################################################################

# native code directory
set(BASE_PATH_FOR_THIS_MODULE ${PROJECT_SOURCE_DIR}/InteropAssemblies/NanoFramwwork.Camera)


# set include directories
list(APPEND NanoFramwwork.Camera_INCLUDE_DIRS ${PROJECT_SOURCE_DIR}/src/CLR/Core)
list(APPEND NanoFramwwork.Camera_INCLUDE_DIRS ${PROJECT_SOURCE_DIR}/src/CLR/Include)
list(APPEND NanoFramwwork.Camera_INCLUDE_DIRS ${PROJECT_SOURCE_DIR}/src/HAL/Include)
list(APPEND NanoFramwwork.Camera_INCLUDE_DIRS ${PROJECT_SOURCE_DIR}/src/PAL/Include)
list(APPEND NanoFramwwork.Camera_INCLUDE_DIRS ${BASE_PATH_FOR_THIS_MODULE})


# source files
set(NanoFramwwork.Camera_SRCS

    NanoFramwwork_Camera.cpp


    NanoFramwwork_Camera_NanoFramework_Camera_Camera_mshl.cpp
    NanoFramwwork_Camera_NanoFramework_Camera_Camera.cpp

)

foreach(SRC_FILE ${NanoFramwwork.Camera_SRCS})
    set(NanoFramwwork.Camera_SRC_FILE SRC_FILE-NOTFOUND)
    find_file(NanoFramwwork.Camera_SRC_FILE ${SRC_FILE}
        PATHS
	        ${BASE_PATH_FOR_THIS_MODULE}
	        ${TARGET_BASE_LOCATION}
            ${PROJECT_SOURCE_DIR}/src/NanoFramwwork.Camera

	    CMAKE_FIND_ROOT_PATH_BOTH
    )
    # message("${SRC_FILE} >> ${NanoFramwwork.Camera_SRC_FILE}") # debug helper
    list(APPEND NanoFramwwork.Camera_SOURCES ${NanoFramwwork.Camera_SRC_FILE})
endforeach()

include(FindPackageHandleStandardArgs)

FIND_PACKAGE_HANDLE_STANDARD_ARGS(NanoFramwwork.Camera DEFAULT_MSG NanoFramwwork.Camera_INCLUDE_DIRS NanoFramwwork.Camera_SOURCES)
