#!/usr/bin/env ruby

require 'rubygems'
require 'bundler/setup'

require 'xcodeproj'

proj = Xcodeproj::Project.new('Unity-iPhone.xcodeproj')
proj.initialize_from_file

proj.targets.each do |target|
    
    # Delete if the same run script phase exists
    target.build_phases.delete_if do |phase|
        phase.is_a?(Xcodeproj::Project::Object::PBXShellScriptBuildPhase) &&
        phase.name == 'Carthage Copy Frameworks'
    end
    
    # Create a new run script phase
    run_script = proj.new(Xcodeproj::Project::Object::PBXShellScriptBuildPhase)
    run_script.name = 'Carthage Copy Frameworks'
    run_script.shell_script = '/usr/local/bin/carthage copy-frameworks'
    
    run_script.input_paths = Dir.glob('Carthage/Build/iOS/*.framework').map do |framework|
        "$(SRCROOT)/#{framework}"
    end
    
    run_script.output_paths = Dir.glob('Carthage/Build/iOS/*.framework').map do |framework|
        "$(BUILT_PRODUCTS_DIR)/$(FRAMEWORKS_FOLDER_PATH)/#{File.basename(framework)}"
    end
    
    target.build_phases << run_script
end

proj.save
